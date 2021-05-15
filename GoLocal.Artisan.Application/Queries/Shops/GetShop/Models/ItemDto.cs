using System;

namespace GoLocal.Artisan.Application.Queries.Shops.GetShop.Models
{
    public class ItemDto
    {
        public string Id { get; init; }
        
        public string Name { get; init; }
        public string Description { get; init; }
        
        public DateTime Creation { get; init; }
    }
}