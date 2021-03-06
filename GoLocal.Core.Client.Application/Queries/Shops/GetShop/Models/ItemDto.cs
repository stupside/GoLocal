using System;

namespace GoLocal.Core.Client.Application.Queries.Shops.GetShop.Models
{
    public class ItemDto
    {
        public int Id { get; init; }
        public string Image { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public DateTime Creation { get; init; }
    }
}