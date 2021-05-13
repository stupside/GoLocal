using System;

namespace GoLocal.Artisan.Application.Queries.Shops.GetShop.Models
{
    public class ItemDto
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        
        public DateTime Creation { get; }
    }
}