using GoLocal.Artisan.Infrastructure.Accessors;
using GoLocal.Domain.Entities.Identity;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Accessor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Artisan.Infrastructure
{
    public static class DependencyInjection
    {
        public static void SetupInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.SetupEfContext(configuration);
            services.SetupAccessor<UserAccessor, User>();
        }
    }
}