using GoLocal.Shared.Accessor.Accessors;
using Microsoft.Extensions.DependencyInjection;

namespace GoLocal.Shared.Accessor
{
    public static class DependencyInjection
    {
        public static void SetupAccessor<TUserAccessor, TUser>(this IServiceCollection services)
            where TUserAccessor : class, IUserAccessor<TUser>
            where TUser : class
            => services.SetupAccessor<IUserAccessor<TUser>, TUserAccessor, TUser>();
        
        public static void SetupAccessor<TIUserAccessor, TUserAccessor, TUser>(this IServiceCollection services)
            where TIUserAccessor : class, IUserAccessor<TUser>
            where TUserAccessor : class, TIUserAccessor
            where TUser : class
        {
            services.AddHttpContextAccessor();
            
            services.AddTransient<TIUserAccessor, TUserAccessor>();
        }
    }
}