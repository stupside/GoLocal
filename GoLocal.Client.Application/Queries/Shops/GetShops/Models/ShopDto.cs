using System;

namespace GoLocal.Client.Application.Queries.Shops.GetShops.Models
{
    public class ShopDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public DateTime Creation { get; init; }
    }
}