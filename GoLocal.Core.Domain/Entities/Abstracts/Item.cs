using System;
using System.Collections.Generic;

namespace GoLocal.Core.Domain.Entities.Abstracts
{
    public abstract class Item
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        
        public byte[] Image { get; set; }
        
        public bool Hidden { get; set; }
        public bool Available { get; set; }
        
        public DateTime Creation { get; }

        public Item()
        {
            Creation = DateTime.UtcNow;
        }

        public Item(Shop shop, string name, string description, bool hidden = false, bool available = true)
            : this()
        {
            ShopId = shop.Id;
            
            Name = name;
            Description = description;
            Hidden = hidden;
            Available = available;
        }
        
        public int ShopId { get; }
        public virtual Shop Shop { get; }
        
        public virtual ICollection<Package> Packages { get; }
        
        public virtual ICollection<Comment> Comments { get; }
    }
}