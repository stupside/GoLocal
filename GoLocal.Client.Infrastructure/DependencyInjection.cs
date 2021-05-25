using GoLocal.Client.Infrastructure.Accessors;
using GoLocal.Domain.Entities.Identity;
using GoLocal.Persistence;
using GoLocal.Shared.Accessor;
using GoLocal.Shared.Locate;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Client.Infrastructure
{
    public static class DependencyInjection
    {
        public static void SetupInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.SetupLocate(configuration);
            services.SetupPersistence(configuration);
            services.SetupAccessor<UserAccessor, User>();
        }
    }
}