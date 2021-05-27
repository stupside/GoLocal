using System;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Identity.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GoLocal.Identity.Infrastructure.Commons.Accessors
{
    public class UserAccessor : IUserAccessor<User>
    {
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<User> _manager;
        public UserAccessor(UserManager<User> manager, IHttpContextAccessor http)
        {
            _manager = manager;
            _http = http;
        }
        
        public Task<User> GetUserAsync()
            => _manager.GetUserAsync(_http.HttpContext?.User) 
               ?? throw new NullReferenceException(nameof(User));

        public async Task<string> GetUserIdAsync()
            => (await GetUserAsync()).Id;
    }
}