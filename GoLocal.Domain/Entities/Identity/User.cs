using GoLocal.Domain.ValueObjects;

namespace GoLocal.Domain.Entities.Identity
{
    public class User
    {
        public string Id { get; set; }
        
        public string UserName { get; set; }
        
        public string Email { get; set; }
        
        public string Phone { get; set; }
        
        public Image Avatar { get; set; }

        public User() {}

        public User(string id, string username, string email, string phone = null, Image avatar = null)
        {
            Id = id;
            UserName = username;
            Email = email;
            Phone = phone;
            Avatar = avatar;
        }
    }
}