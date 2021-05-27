using GoLocal.Bus.Authorizer;
using GoLocal.Core.Client.Infrastructure.Accessors;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Persistence;
using GoLocal.Core.Persistence.EntityFramework;
using GoLocal.Shared.Locate;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Core.Client.Infrastructure
{
    public static class DependencyInjection
    {
        public static void SetupInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.SetupAuthorizer<UserAccessor, User, Context>(typeof(DependencyInjection).Assembly);
            services.SetupLocate(configuration);
            services.SetupPersistence(configuration);
        }
    }
}