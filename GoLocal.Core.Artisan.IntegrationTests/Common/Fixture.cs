using System.IO;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Core.Artisan.Application;
using GoLocal.Core.Artisan.Infrastructure;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Persistence.EntityFramework;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GoLocal.Core.Artisan.IntegrationTests.Common
{
    public class Fixture
    {
        protected readonly IMediator Mediator;
        protected readonly Context Context;
        
        protected Fixture()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .AddUserSecrets<Fixture>();
            
            IConfiguration configuration = builder.Build();
            
            var services = new ServiceCollection();

            services.AddOptions();
            services.SetupInfrastructure(configuration);
            services.SetupApplication();
            
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            
            services.AddTransient<IUserAccessor<User>, UserAccesor>();
            
            ServiceProvider provider = services.BuildServiceProvider();
            
            Context = provider.GetService<Context>();
            Mediator = provider.GetService<IMediator>();


        }
    }
}