using GoLocal.Shared.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Identity.Application
{
    public static class DependencyInjection
    {
        public static void SetupApplication(this IServiceCollection services)
        {
            services.SetupBus(typeof(DependencyInjection).Assembly);
        }    
    }
}