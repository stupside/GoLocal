using Microsoft.AspNetCore.Identity;

namespace GoLocal.Identity.Domain.Entities
{
    public sealed class User : IdentityUser
    {
        public string Avatar { get; set; }
    
        public User(){}

        public User(string email, string username)
        {
            Email = email;
            UserName = username;
        }
    }
}