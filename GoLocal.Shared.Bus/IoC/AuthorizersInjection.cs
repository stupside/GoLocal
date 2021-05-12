using System.Reflection;
using GoLocal.Shared.Bus.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Shared.Bus.IoC
{
    internal static class AuthorizersInjection
    {
        internal static void SetupAuthorizers(this IServiceCollection services, Assembly assembly)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
        }
    }
}