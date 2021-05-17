using GoLocal.Persistence.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Persistence
{
    public static class DependencyInjection
    {
        public static void SetupPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.SetupEfContext(configuration);
        }
    }
}