using System;
using System.Threading.Tasks;
using GoLocal.Identity.Application.Commons.Accessor;
using GoLocal.Identity.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GoLocal.Identity.Infrastructure.Commons.Accessors
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _http;

        public UserAccessor(UserManager<User> manager, IHttpContextAccessor http)
        {
            Manager = manager;
            _http = http;
        }

        public UserManager<User> Manager { get; }
        
        public Task<User> GetUserAsync()
            => Manager.GetUserAsync(_http.HttpContext?.User) 
               ?? throw new NullReferenceException(nameof(User));
    }
}