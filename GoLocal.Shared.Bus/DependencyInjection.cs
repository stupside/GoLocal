using System.Reflection;
using GoLocal.Shared.Bus.Behaviours;
using GoLocal.Shared.Bus.IoC;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Shared.Bus
{
    public static class DependencyInjection
    {
        public static void SetupBus(this IServiceCollection services, Assembly assembly)
        {
            services.AddTransient(typeof(IRequestPreProcessor<>), typeof(LoggingBehavior<>));
            
            services.AddMediatR(assembly);
            
            services.SetupAuthorizers(assembly);
            services.SetupValidators(assembly);
        }
    }
}