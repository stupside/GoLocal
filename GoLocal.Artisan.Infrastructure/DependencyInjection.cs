using GoLocal.Artisan.Infrastructure.Accessors;
using GoLocal.Domain.Entities.Identity;
using GoLocal.Persistence;
using GoLocal.Shared.Accessor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Artisan.Infrastructure
{
    public static class DependencyInjection
    {
        public static void SetupInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.SetupPersistence(configuration);
            services.SetupAccessor<UserAccessor, User>();
        }
    }
}