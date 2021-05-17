using System;

namespace GoLocal.Client.Application.Queries.GetShop.Models
{
    public class ItemDto
    {
        public int Id { get; init; }
        
        public string Name { get; init; }
        public string Description { get; init; }
        
        public DateTime Creation { get; init; }
    }
}