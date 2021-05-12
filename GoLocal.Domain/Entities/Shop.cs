using System;
using System.Collections.Generic;
using GoLocal.Domain.Entities.Identity;
using GoLocal.Domain.ValueObjects;

namespace GoLocal.Domain.Entities
{
    public class Shop
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Tag { get; set; }
        
        public Localisation Localisation { get; }
        public Contact Contact { get; }
        
        public DateTime Creation { get; }

        public Shop()
        {
            Localisation = new Localisation();
            Contact = new Contact();
            
            Creation = DateTime.UtcNow;
        }

        public Shop(User user, string name, string tag)
            : this()
        {
            UserId = user.Id;
            
            Name = name;
            Tag = tag;
        }
        
        public string UserId { get; init; }
        public virtual User User { get; }

        public virtual ICollection<Opening> Openings { get; }

        public virtual ICollection<Service> Services { get; }
        public virtual ICollection<Product> Products { get; }
        
        public virtual ICollection<ShopCategory> ShopCategories { get; }
    }
}