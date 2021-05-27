using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.AspNetCore.Http;

namespace GoLocal.Core.Client.Infrastructure.Accessors
{
    public class UserAccessor : IUserAccessor<User>
    {
        private readonly IHttpContextAccessor _http;
        private readonly Context _context;

        public UserAccessor(Context context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }

        public async Task<User> GetUserAsync()
        {
            if (_http.HttpContext.User.Identity == null)
                throw new NullReferenceException();

            if (!_http.HttpContext.User.Identity.IsAuthenticated)
                throw new InvalidConstraintException();

            string uid = _http.HttpContext.User.Claims.First(m => m.Type == "sub").Value;
            if (string.IsNullOrEmpty(uid))
                throw new ArgumentNullException(nameof(uid));

            User user =  await _context.Users.FindAsync(uid);
            if (user != null) return user;

            string email = _http.HttpContext.User.Claims.First(m => m.Type == "email").Value;
            if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(email))
                throw new ArgumentNullException();

            user = new User(uid, _http.HttpContext.User.Identity.Name, email);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<string> GetUserIdAsync()
            => (await GetUserAsync()).Id;
    }
}