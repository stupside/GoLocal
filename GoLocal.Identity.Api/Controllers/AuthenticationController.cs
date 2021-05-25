

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GoLocal.Identity.Domain.Entities;
using GoLocal.Identity.Infrastructure.Helpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace GoLocal.Identity.Api.Controllers
{
    [Route("/connect")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IOpenIddictScopeManager _scope;
        private readonly IOpenIddictApplicationManager _application;
        private readonly IOpenIddictAuthorizationManager _authorization;
        
        private readonly SignInManager<User> _sign;
        private readonly UserManager<User> _user;
        
        public AuthenticationController(
            SignInManager<User> sign,
            UserManager<User> user, IOpenIddictAuthorizationManager authorization, IOpenIddictApplicationManager application, IOpenIddictScopeManager scope)
        {
            _sign = sign;
            _user = user;
            _authorization = authorization;
            _application = application;
            _scope = scope;
        }
        
        [HttpGet("authorize")]
        [HttpPost("authorize")]
        public async Task<IActionResult> Authorize()
        {
            var request = HttpContext.GetOpenIddictServerRequest() ??
                          throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");
            
            if (string.IsNullOrEmpty(request.ClientId))
                throw new InvalidOperationException("Details concerning the calling client application are not valid.");
            
            var result = await HttpContext.AuthenticateAsync(IdentityConstants.ApplicationScheme);
            
            if (!result.Succeeded || (request.MaxAge != null && result.Properties?.IssuedUtc != null && DateTimeOffset.UtcNow - result.Properties.IssuedUtc > TimeSpan.FromSeconds(request.MaxAge.Value)))
                return Challenge(
                    authenticationSchemes: IdentityConstants.ApplicationScheme,
                    properties: new AuthenticationProperties
                    {
                        RedirectUri = Request.PathBase + Request.Path + QueryString.Create(
                            Request.HasFormContentType ? Request.Form.ToList() : Request.Query.ToList())
                    });

            var user = await _user.GetUserAsync(result.Principal) ??
                       throw new InvalidOperationException("The user details cannot be retrieved.");

            var application = await _application.FindByClientIdAsync(request.ClientId);
            if(application == null)
                throw new InvalidOperationException("Details concerning the calling client application cannot be found.");
            
            var principal = await _sign.CreateUserPrincipalAsync(user) ??
                            throw new InvalidOperationException("Cannot construct principal for the current user.");
            
            principal.SetScopes(request.GetScopes());
            principal.SetResources(await _scope.ListResourcesAsync(request.GetScopes()).ToListAsync());
            
            foreach (var claim in principal.Claims)
                claim.SetDestinations(GetDestinations(claim, principal));
            
            var authorization = await _authorization.CreateAsync(
                principal: principal,
                subject  : user.Id,
                client   : await _application.GetIdAsync(application) ?? throw new InvalidOperationException(),
                type     : OpenIddictConstants.AuthorizationTypes.Permanent,
                scopes   : principal.GetScopes());

            principal.SetAuthorizationId(await _authorization.GetIdAsync(authorization));

            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
        
        [HttpPost("token")]
        public async Task<IActionResult> Exchange()
        {
            var request = HttpContext.GetOpenIddictServerRequest() ??
                throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

            return await PasswordFlow(request);

            //return await CodeFlow(request);
        }
        
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _sign.SignOutAsync();
            return SignOut(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
        
        [HttpGet("userinfo")]
        public async Task<IActionResult> UserInfo()
        {
            var principal = (await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)).Principal;
            if (principal == null)
                return NotFound();

            User user = await _user.GetUserAsync(principal);
            
            return Ok(new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.Avatar,
                user.PhoneNumber,
                user.TwoFactorEnabled,
                user.PhoneNumberConfirmed
            });
        }

        private async Task<IActionResult> CodeFlow(OpenIddictRequest request)
        {
            if (!request.IsAuthorizationCodeGrantType() && !request.IsRefreshTokenGrantType())
                throw new InvalidOperationException("The specified grant type is not supported.");
            
            var principal = (await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)).Principal;
            if (principal == null)
                return NotFound();
                
            var user = await _sign.ValidateSecurityStampAsync(principal);
            if (user == null)
            {
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The token is no longer valid."
                    }));
            }

            if (!await _sign.CanSignInAsync(user))
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The user is no longer allowed to sign in."
                    }));

            principal.SetScopes(request.GetScopes());
            principal.SetResources(await _scope.ListResourcesAsync(request.GetScopes()).ToListAsync());
            
            foreach (var claim in principal.Claims)
                claim.SetDestinations(GetDestinations(claim, principal));

            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
        
        private async Task<IActionResult> PasswordFlow(OpenIddictRequest request)
        {
            if(!request.IsPasswordGrantType()) 
                return BadRequest("The specified grant type is not implemented.");
            
            var user = await _user.FindByNameAsync(request.Username);
            if (user == null || !await _sign.CanSignInAsync(user))
            {
                var properties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The username/password couple is invalid"
                });

                return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }
                

            // Validate the username/password parameters and ensure the account is not locked out.
            var result = await _sign.CheckPasswordSignInAsync(user, request.Password, true);
            if (!result.Succeeded)
            {
                var properties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                        "The username/password couple is invalid"
                });

                return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            var principal = await _sign.CreateUserPrincipalAsync(user);
            principal.SetScopes(request.GetScopes());
            principal.SetResources(await _scope.ListResourcesAsync(request.GetScopes()).ToListAsync());
            
            foreach (var claim in principal.Claims)
                claim.SetDestinations(GetDestinations(claim, principal));

            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
        
        private static IEnumerable<string> GetDestinations(Claim claim, ClaimsPrincipal principal)
        {
            switch (claim.Type)
            {
                case OpenIddictConstants.Claims.Name:
                    yield return OpenIddictConstants.Destinations.AccessToken;

                    if (principal.HasScope(OpenIddictConstants.Permissions.Scopes.Profile))
                        yield return OpenIddictConstants.Destinations.IdentityToken;

                    yield break;

                case OpenIddictConstants.Claims.Email:
                    yield return OpenIddictConstants.Destinations.AccessToken;

                    if (principal.HasScope(OpenIddictConstants.Permissions.Scopes.Email))
                        yield return OpenIddictConstants.Destinations.IdentityToken;

                    yield break;

                case OpenIddictConstants.Claims.Role:
                    yield return OpenIddictConstants.Destinations.AccessToken;

                    if (principal.HasScope(OpenIddictConstants.Permissions.Scopes.Roles))
                        yield return OpenIddictConstants.Destinations.IdentityToken;

                    yield break;

                case "AspNet.Identity.SecurityStamp": yield break;

                default:
                    yield return OpenIddictConstants.Destinations.AccessToken;
                    yield break;
            }
        }
    }
}