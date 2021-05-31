using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Core.Domain.Entities.Identity;

namespace GoLocal.Core.Artisan.IntegrationTests.Common
{
    public class UserAccesor : IUserAccessor<User>
    {
        public Task<User> GetUserAsync()
        {
            return Task.FromResult(new User
            {
                Id = "Test",
                UserName = "Test",
                Email = "test@test.com",
                Phone = "test",
                Avatar = null,
            });
        }

        public Task<string> GetUserIdAsync()
        {
            return Task.FromResult("Test");
        }

        public bool IsAuthenticated()
        {
            return true;
        }
    }
}