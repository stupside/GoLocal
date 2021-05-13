using GoLocal.Identity.Application.Commons.Accessor;
using GoLocal.Identity.Domain.Entities;
using GoLocal.Identity.Infrastructure.Commons.Accessors;
using GoLocal.Identity.Infrastructure.IoC;
using GoLocal.Identity.Infrastructure.Persistence.EntityFramework;
using GoLocal.Shared.Accessor;
using GoLocal.Shared.Mailing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Identity.Infrastructure
{
    public static class DependencyInjection
    {
        public static void SetupInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.SetupEfContext(configuration);
            services.SetupAccessor<IUserAccessor, UserAccessor, User>();
            
            services.SetupMailing(configuration);
            
            services.SetupOidc(configuration);
        }
    }
}