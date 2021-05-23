using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Identity.Infrastructure.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;

namespace GoLocal.Identity.Infrastructure.Commons.Oidc
{
    public class Worker : IHostedService
    {
        private readonly IServiceProvider _provider; 

        public Worker(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _provider.CreateScope();

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            // TODO: Get from configuration
            await manager.AddClient(m => {
                m.ClientId = "golocal";
                
                m.PostLogoutRedirectUris.Add(new Uri("https://localhost:5000"));
                
                m.RedirectUris.Add(new Uri("https://localhost:3000/authentication/silent_callback"));
                m.RedirectUris.Add(new Uri("https://localhost:3000/authentication/callback"));
                
                m.RedirectUris.Add(new Uri("https://localhost:3001/authentication/silent_callback"));
                m.RedirectUris.Add(new Uri("https://localhost:3001/authentication/callback"));
                
                m.RedirectUris.Add(new Uri("https://localhost:3002/authentication/silent_callback"));
                m.RedirectUris.Add(new Uri("https://localhost:3002/authentication/callback"));
                
                m.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Authorization);
                m.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Logout);
                m.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Token);
                m.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.Password);
                m.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode);
                m.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.RefreshToken);
                m.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.Code);
                m.Permissions.Add(OpenIddictConstants.Permissions.Scopes.Profile);
                m.Permissions.Add(OpenIddictConstants.Permissions.Scopes.Roles);

                m.Permissions.Add(OpenIddictConstants.Permissions.Prefixes.Scope + "account.api");
                m.Permissions.Add(OpenIddictConstants.Permissions.Prefixes.Scope + "client.api");
                m.Permissions.Add(OpenIddictConstants.Permissions.Prefixes.Scope + "artisan.api");

                m.Requirements.Add(OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange);
            }, cancellationToken);

            await CreateScopesAsync(scope, cancellationToken);
        }

        private static async Task CreateScopesAsync(IServiceScope scope, CancellationToken cancellationToken)
        {
            HashSet<string> scopes = new HashSet<string>
            {
                "account.api",
                "client.api",
                "artisan.api"
            };
            
            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();
            
            foreach (string s in scopes)
            {
                if (await manager.FindByNameAsync(s, cancellationToken) != null) continue;
                
                var descriptor = new OpenIddictScopeDescriptor
                {
                    Name = s,
                    Resources = { s }
                };

                await manager.CreateAsync(descriptor, cancellationToken);
            }
        }
        
        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}