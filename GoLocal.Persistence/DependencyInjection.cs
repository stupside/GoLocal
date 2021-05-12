using GoLocal.Persistence.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Persistence
{
    public static class DependencyInjection
    {
        public static void SetupInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.SetupEfContext(configuration);
        }
    }
}