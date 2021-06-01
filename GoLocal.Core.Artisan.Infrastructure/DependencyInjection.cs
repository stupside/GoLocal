using GoLocal.Bus.Authorizer;
using GoLocal.Core.Artisan.Infrastructure.Accessors;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Persistence;
using GoLocal.Core.Persistence.EntityFramework;
using GoLocal.Shared.Locate;
using GoLocal.Shared.Mailing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Core.Artisan.Infrastructure
{
    public static class DependencyInjection
    {
        public static void SetupInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.SetupPersistence(configuration);
            services.SetupAuthorizer<UserAccessor, User, Context>(typeof(DependencyInjection).Assembly);
            
            services.SetupMailing(configuration);
            services.SetupLocate(configuration);
        }
    }
}