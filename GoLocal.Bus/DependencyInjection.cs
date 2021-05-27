using System.Reflection;
using FluentValidation;
using GoLocal.Bus.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Bus
{
    public static class DependencyInjection
    {
        public static void SetupBus(this IServiceCollection services, Assembly assembly)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            
            services.AddMediatR(assembly);
            services.AddValidatorsFromAssembly(assembly);
        }
    }
}