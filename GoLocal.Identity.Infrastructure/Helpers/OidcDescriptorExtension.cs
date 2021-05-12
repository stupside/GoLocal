using System;
using System.Threading;
using System.Threading.Tasks;
using OpenIddict.Abstractions;

namespace GoLocal.Identity.Infrastructure.Helpers
{
    public static class OidcDescriptorExtension
    {
        public static async Task AddSpaAsync(this IOpenIddictApplicationManager manager, string id, string logout, string url, CancellationToken cancellationToken)
        {
            if (await manager.FindByClientIdAsync(id, cancellationToken) is null)
                return;
            
            var application = new OpenIddictApplicationDescriptor
            {
                ClientId = id,
                PostLogoutRedirectUris =
                {
                    new Uri(logout)
                },
                RedirectUris =
                {
                    new Uri($"{url}/authentication/silent_callback"),
                    new Uri($"{url}/authentication/callback")
                },
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                    OpenIddictConstants.Permissions.Endpoints.Logout,
                    OpenIddictConstants.Permissions.Endpoints.Token,

                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken,

                    OpenIddictConstants.Permissions.ResponseTypes.Code,

                    OpenIddictConstants.Permissions.Scopes.Profile,
                    OpenIddictConstants.Permissions.Scopes.Roles
                },
                Requirements =
                {
                    OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
                }
            };
            
            await manager.CreateAsync(application, cancellationToken);
        }
    }
}