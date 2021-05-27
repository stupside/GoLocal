using GoLocal.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Core.Artisan.Application
{
    public static class DependencyInjection
    {
        public static void SetupApplication(this IServiceCollection services)
        {
            services.SetupBus(typeof(DependencyInjection).Assembly);
        }
    }
}