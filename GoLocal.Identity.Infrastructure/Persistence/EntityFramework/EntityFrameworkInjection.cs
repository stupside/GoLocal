using GoLocal.Identity.Application.Persistence;
using GoLocal.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Identity.Infrastructure.Persistence.EntityFramework
{
    public static class EntityFrameworkInjection
    {
        public static void SetupIdentityContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(options =>
                options.UseNpgsql(configuration.GetConnectionString(nameof(Context)),
                    b => b.MigrationsAssembly(typeof(Context).Assembly.FullName)));

            services.AddScoped<IContext>(provider => provider.GetService<Context>());

            services.Configure<IdentityOptions>(configuration.GetSection("IdentityConfiguration"));
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<Context>()
                .AddDefaultTokenProviders();
            
            services.AddDbContext<OidcContext>(m =>
            {
                m.UseInMemoryDatabase(nameof(OidcContext));
                m.UseOpenIddict();
            });
        }
    }
}