using System.Threading.Tasks;

namespace GoLocal.Shared.Accessor.Accessors
{
    public interface IUserAccessor<TUser>
        where TUser : class
    {
        Task<TUser> GetUserAsync();
    }
}