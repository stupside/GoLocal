using System;
using System.Collections.Generic;
using GoLocal.Core.Domain.Entities.Identity;

namespace GoLocal.Core.Domain.Entities
{
    public class Cart
    {
        public string Id { get; set; }
        
        public DateTime Creation { get; }

        public Cart()
        {
            Creation = DateTime.UtcNow;
        }
        
        public Cart(User user, int shopId)
            : this()
        {
            UserId = user.Id;
            ShopId = shopId;
        }
        
        public string UserId { get; }
        
        public int ShopId { get; }
        public virtual Shop Shop { get; }
        
        public virtual ICollection<CartPackage> CartPackages { get; }
    }
}