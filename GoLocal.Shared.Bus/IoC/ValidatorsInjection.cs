using System.Reflection;
using FluentValidation;
using GoLocal.Shared.Bus.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Shared.Bus.IoC
{
    internal static class ValidatorsInjection
    {
        internal static void SetupValidators(this IServiceCollection services, Assembly assembly)
        {
            services.AddValidatorsFromAssembly(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}