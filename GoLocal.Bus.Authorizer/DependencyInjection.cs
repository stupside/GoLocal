using System.Linq;
using System.Reflection;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Authorizer.Authorizers;
using GoLocal.Bus.Authorizer.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Bus.Authorizer
{
    public static class DependencyInjection
    {
        public static void SetupAuthorizer<TUserAccessor, TUser, TContext>(this IServiceCollection services, Assembly assembly)
            where TUserAccessor : class, IUserAccessor<TUser>
            where TUser : class
            where TContext : DbContext
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IUserAccessor<TUser>, TUserAccessor>();

            services.AddScoped(typeof(IAuthorizerHandler), typeof(AuthorizerHandler<TUser, TContext>));

            var types = assembly.GetTypes();
            
            var configurations = types
                .Where(m => m.GetInterfaces().Any(r => r == typeof(IAuthorizerConfiguration)) && !m.IsAbstract && !m.IsInterface && m.IsClass);

            foreach (var configuration in configurations)
                services.AddSingleton(typeof(IAuthorizerConfiguration), configuration);
        }
    }
}