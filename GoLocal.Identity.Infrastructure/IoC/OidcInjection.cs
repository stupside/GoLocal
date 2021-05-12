using System;
using System.IdentityModel.Tokens.Jwt;
using GoLocal.Identity.Infrastructure.Commons.Oidc;
using GoLocal.Identity.Infrastructure.Persistence.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;

namespace GoLocal.Identity.Infrastructure.IoC
{
    internal static class OidcInjection
    {
        internal static void SetupOidc(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                        
            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIddictConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIddictConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIddictConstants.Claims.Role;
                options.ClaimsIdentity.EmailClaimType = OpenIddictConstants.Claims.Email;
            });

            services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                        .UseDbContext<OidcContext>();
                })
                .AddServer(options =>
                {
                    options.SetAuthorizationEndpointUris("/connect/authorize")
                        .SetLogoutEndpointUris("/connect/logout")
                        .SetTokenEndpointUris("/connect/token")
                        .SetUserinfoEndpointUris("/connect/userinfo")
                        .SetAccessTokenLifetime(TimeSpan.FromMinutes(30));

                    options.AllowPasswordFlow()
                        .AcceptAnonymousClients();

                    options
                        .AllowAuthorizationCodeFlow()
                        .RequireProofKeyForCodeExchange();

                    options.AddEncryptionKey(new SymmetricSecurityKey(Convert.FromBase64String("DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=")));

                    options.AddDevelopmentSigningCertificate();
                    options.AddDevelopmentEncryptionCertificate();
                    
                    options.DisableAccessTokenEncryption();

                    options
                        .UseAspNetCore()
                        .EnableAuthorizationEndpointPassthrough()
                        .EnableLogoutEndpointPassthrough()
                        .EnableTokenEndpointPassthrough()
                        .EnableUserinfoEndpointPassthrough();
                    
                })
                .AddValidation(options =>
                {
                    options.UseLocalServer();
                    options.UseAspNetCore();
                });
            
            services.AddAuthentication();

            services.AddHostedService<Worker>();
        }
    }
}