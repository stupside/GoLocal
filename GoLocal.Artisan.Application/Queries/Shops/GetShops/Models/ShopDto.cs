using System;

namespace GoLocal.Artisan.Application.Queries.Shops.GetShops.Models
{
    public class ShopDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Image { get; init; }
        public LocationDto Location { get; init; }
        public DateTime Creation { get; init; }
    }
}