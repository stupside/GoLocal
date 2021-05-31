using System;
using System.Collections.Generic;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Domain.ValueObjects;

namespace GoLocal.Core.Domain.Entities
{
    public class Shop
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public byte[] Image { get; set; }
        
        public Location Location { get; }
        public Contact Contact { get; }
        
        public Visibility Visibility { get; set; }
        
        public DateTime Creation { get; }

        public Shop()
        {
            Visibility = Visibility.Public;
            Creation = DateTime.UtcNow;
        }

        public Shop(User user, string name, Contact contact, Location location)
            : this()
        {
            UserId = user.Id;
            
            Name = name;
            Contact = contact;
            Location = location;
        }
        
        public string UserId { get; set; }
        public virtual User User { get; }
        
        public virtual ICollection<Employee> Employees { get; }

        public virtual ICollection<Opening> Openings { get; }

        public virtual ICollection<Service> Services { get; }
        public virtual ICollection<Product> Products { get; }
        
        public virtual ICollection<ShopCategory> ShopCategories { get; }
        
        public virtual ICollection<Command> Commands { get; }
        public virtual ICollection<Invoice> Invoices { get; }
    }
}