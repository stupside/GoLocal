using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Artisan.IntegrationTests.Commons
{
    public class UserAccessor : IUserAccessor<User>
    {
        private readonly Context _context;

        public UserAccessor(Context context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync()
        {
            return await _context.Users.FirstOrDefaultAsync();
        }

        public async Task<string> GetUserIdAsync()
        {
            return (await GetUserAsync()).Id;
        }

        public bool IsAuthenticated()
        {
            return true;
        }
    }
}