using System;
using System.Collections.Generic;
using GoLocal.Core.Client.Application.Queries.Shops.GetShop.Models;

namespace GoLocal.Core.Client.Application.Queries.Shops.GetShop
{
    public class GetShopResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        
        public string Image { get; init; }
        public double Rate { get; init; }
        
        public LocationDto Location { get; init; }
        public ContactDto Contact { get; init; }
        
        public ICollection<ItemDto> Products { get; init; }
        public ICollection<ItemDto> Services { get; init; }
        
        public ICollection<OpeningDto> Openings { get; init; }
        
        public UserDto User { get; init; }
        
        public DateTime Creation { get; init; }
    }
}