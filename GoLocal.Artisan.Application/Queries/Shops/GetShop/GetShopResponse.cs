using System;
using System.Collections.Generic;
using GoLocal.Artisan.Application.Queries.Shops.GetShop.Models;

namespace GoLocal.Artisan.Application.Queries.Shops.GetShop
{
    public class GetShopResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        
        public LocalisationDto Localisation { get; init; }
        public ContactDto Contact { get; init; }
        
        public ICollection<ItemDto> Products { get; init; }
        public ICollection<ItemDto> Services { get; init; }
        
        public ICollection<OpeningDto> Openings { get; init; }
        
        public ICollection<UserDto> Employees { get; init; }
        
        public UserDto User { get; init; }
        
        public DateTime Creation { get; init; }
    }
}