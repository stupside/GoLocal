using System.Threading.Tasks;

namespace GoLocal.Bus.Authorizer.Accessors
{
    public interface IUserAccessor<TUser>
    {
        Task<TUser> GetUserAsync();
        Task<string> GetUserIdAsync();
    }
}