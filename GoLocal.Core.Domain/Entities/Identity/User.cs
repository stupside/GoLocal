using System.Collections.Generic;

namespace GoLocal.Core.Domain.Entities.Identity
{
    public class User
    {
        public string Id { get; set; }
        public string Avatar { get; set; }
        
        public string UserName { get; set; }
        
        public string Email { get; set; }
        
        public string Phone { get; set; }

        public User() {}

        public User(string id, string username, string email, string phone = null, string avatar = null)
        {
            Id = id;
            UserName = username;
            Email = email;
            Phone = phone;
            Avatar = avatar;
        }
        
        public virtual ICollection<Shop> Shops { get; }
        public virtual ICollection<Employee> Employments { get; }
        
        public virtual ICollection<Cart> Carts { get; }
        public virtual ICollection<Command> Commands { get; }
        
        public virtual ICollection<Invoice> Invoices { get; }
    }
}