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

            await manager.AddClient(m => {
                m.ClientId = "golocal.account";
                
                m.PostLogoutRedirectUris.Add(new Uri("https://account.golocal.com/login"));
                
                m.RedirectUris.Add(new Uri("https://account.spa.com/authentication/silent_callback"));
                m.RedirectUris.Add(new Uri("https://account.spa.com/authentication/callback"));
                
                
                m.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Authorization);
                m.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Logout);
                m.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Token);
                m.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode);
                m.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.RefreshToken);
                m.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.Code);
                m.Permissions.Add(OpenIddictConstants.Permissions.Scopes.Profile);
                m.Permissions.Add(OpenIddictConstants.Permissions.Scopes.Roles);

                m.Permissions.Add(OpenIddictConstants.Permissions.Prefixes.Scope + "account.api");

                m.Requirements.Add(OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange);
            }, cancellationToken);

            await manager.AddClient(m => {
                m.ClientId = "golocal.artisan";
                
                m.PostLogoutRedirectUris.Add(new Uri("https://account.golocal.com/login"));
                m.RedirectUris.Add(new Uri("https://artisan.spa.com/authentication/silent_callback"));
                m.RedirectUris.Add(new Uri("https://artisan.spa.com/authentication/callback"));
                
                
                m.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Authorization);
                m.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Logout);
                m.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Token);
                m.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.Password);
                m.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode);
                m.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.RefreshToken);
                m.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.Code);
                m.Permissions.Add(OpenIddictConstants.Permissions.Scopes.Profile);
                m.Permissions.Add(OpenIddictConstants.Permissions.Scopes.Roles);

                m.Permissions.Add(OpenIddictConstants.Permissions.Prefixes.Scope + "artisan.api");

                m.Requirements.Add(OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange);
            }, cancellationToken);
            
            await manager.AddClient(m => {
                m.ClientId = "golocal.client";

                m.PostLogoutRedirectUris.Add(new Uri("https://account.golocal.com/login"));
                
                m.RedirectUris.Add(new Uri("https://client.spa.com/authentication/silent_callback"));
                m.RedirectUris.Add(new Uri("https://client.spa.com/authentication/callback"));
                
                m.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Authorization);
                m.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Logout);
                m.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Token);
                m.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode);
                m.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.RefreshToken);
                m.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.Code);
                m.Permissions.Add(OpenIddictConstants.Permissions.Scopes.Profile);
                m.Permissions.Add(OpenIddictConstants.Permissions.Scopes.Roles);

                m.Permissions.Add(OpenIddictConstants.Permissions.Prefixes.Scope + "client.api");

                m.Requirements.Add(OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange);
            }, cancellationToken);
            
            await CreateScopesAsync(scope, cancellationToken);
        }

        private static async Task CreateScopesAsync(IServiceScope scope, CancellationToken cancellationToken)
        {
            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

            if (await manager.FindByNameAsync("artisan.api", cancellationToken) == null)
            {
                var descriptor = new OpenIddictScopeDescriptor
                {
                    Name = "artisan.api",
                    Description = "API used by artisans",
                    Resources =
                    {
                        "golocal.artisan.api"
                    }
                };

                await manager.CreateAsync(descriptor, cancellationToken);
            }

            if (await manager.FindByNameAsync("client.api", cancellationToken) == null)
            {
                var descriptor = new OpenIddictScopeDescriptor
                {
                    Name = "client.api",
                    Description = "API used by clients",
                    Resources =
                    {
                        "golocal.client.api"
                    }
                };

                await manager.CreateAsync(descriptor, cancellationToken);
            }
            
            if (await manager.FindByNameAsync("account.api", cancellationToken) == null)
            {
                var descriptor = new OpenIddictScopeDescriptor
                {
                    Name = "account.api",
                    Description = "API used by clients to manage their accounts",
                    Resources =
                    {
                        "golocal.account.api"
                    }
                };

                await manager.CreateAsync(descriptor, cancellationToken);
            }
        }
        
        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}