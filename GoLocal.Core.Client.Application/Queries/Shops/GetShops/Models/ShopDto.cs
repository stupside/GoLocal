using System;

namespace GoLocal.Core.Client.Application.Queries.Shops.GetShops.Models
{
    public class ShopDto
    {
        public int Id { get; init; }
        public string Image { get; init; }
        public string Name { get; init; }
        public LocationDto Location { get; init; }
        public UserDto User { get; init; }
        public DateTime Creation { get; init; }
    }
}