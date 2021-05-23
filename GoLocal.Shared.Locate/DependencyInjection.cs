using GoLocal.Shared.Locate.Configuration;
using GoLocal.Shared.Locate.Implementations;
using GoLocal.Shared.Locate.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Shared.Locate
{
    public static class DependencyInjection
    {
        public static void SetupLocate(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LocateConfiguration>(configuration.GetSection(nameof(LocateConfiguration)));
            services.AddSingleton<ILocateService, LocateService>();
        }
    }
}