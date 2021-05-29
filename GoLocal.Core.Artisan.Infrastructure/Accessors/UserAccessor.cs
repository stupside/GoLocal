using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Artisan.Infrastructure.Accessors
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
            if (_http.HttpContext.User.Identity is not {IsAuthenticated: true})
                return null;

            string uid = _http.HttpContext.User.Claims.SingleOrDefault(m => m.Type == "sub")?.Value;
            if (string.IsNullOrEmpty(uid))
                throw new ArgumentNullException(nameof(uid));
            
            string email = _http.HttpContext.User.Claims.SingleOrDefault(m => m.Type == "email")?.Value;
            if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(email))
                throw new ArgumentNullException();
            
            User user =  await _context.Users.SingleOrDefaultAsync(m => m.Id == uid);
            if (user != null)
            {
                if (user.Email != email)
                    user.Email = email;

                if (user.UserName != _http.HttpContext.User.Identity.Name)
                    user.UserName = _http.HttpContext.User.Identity.Name;
                
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                
                return user;
            }

            user = new User(uid, _http.HttpContext.User.Identity.Name, email);
            
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            return user;
        }

        public async Task<string> GetUserIdAsync()
        {
            User user = await GetUserAsync();
            return user.Id;
        }
    }
}