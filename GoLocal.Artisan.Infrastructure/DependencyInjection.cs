using GoLocal.Artisan.Infrastructure.Accessors;
using GoLocal.Domain.Entities.Identity;
using GoLocal.Persistence;
using GoLocal.Shared.Accessor;
using GoLocal.Shared.Locate;
using GoLocal.Shared.Mailing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Artisan.Infrastructure
{
    public static class DependencyInjection
    {
        public static void SetupInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.SetupLocate(configuration);
            services.SetupMailing(configuration);
            services.SetupPersistence(configuration);
            services.SetupAccessor<UserAccessor, User>();
        }
    }
}