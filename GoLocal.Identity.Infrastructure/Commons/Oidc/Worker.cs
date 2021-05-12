using System;
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

            await manager.AddSpaAsync("account.golocal",
                "https://web.golocal.com",
                "https://account.golocal.com",
                cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}