using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Persistence.EntityFramework
{
    public static class EntityFrameworkInjection
    {
        internal static void SetupEfContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(options =>
                options.UseNpgsql(configuration.GetConnectionString(nameof(Context)),
                    b => b.MigrationsAssembly(typeof(Context).Assembly.FullName)));
        }
    }
}