using System;
using System.Collections.Generic;
using GoLocal.Domain.Entities.Identity;

namespace GoLocal.Domain.Entities
{
    public class Cart
    {
        public string Id { get; set; }
        
        public DateTime Creation { get; }

        public Cart()
        {
            Creation = DateTime.UtcNow;
        }
        
        public Cart(User user, Shop shop)
            : this()
        {
            UserId = user.Id;
            ShopId = shop.Id;
        }
        
        public string UserId { get; }
        
        public int ShopId { get; }
        public virtual Shop Shop { get; }
        
        public virtual ICollection<CartPackage> CartPackages { get; }
    }
}