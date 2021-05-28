using System;
using System.Collections.Generic;
using GoLocal.Core.Domain.Enums;

namespace GoLocal.Core.Domain.Entities.Abstracts
{
    public abstract class Item
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        
        public byte[] Image { get; set; }
        
        public Visibility Visibility { get; set; }
        
        public DateTime Creation { get; }

        public Item()
        {
            Visibility = Visibility.Public;
            Creation = DateTime.UtcNow;
        }

        public Item(Shop shop, string name, string description, bool hidden = false, bool available = true)
            : this()
        {
            ShopId = shop.Id;
            
            Name = name;
            Description = description;
        }
        
        public int ShopId { get; }
        public virtual Shop Shop { get; }
        
        public virtual ICollection<Package> Packages { get; }
        
        public virtual ICollection<Comment> Comments { get; }
    }
}