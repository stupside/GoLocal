using System;
using System.Collections.Generic;

namespace GoLocal.Artisan.Application.Queries.Shops.GetShops.Models
{
    public class ShopDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        
        public LocalisationDto Localisation { get; init; }
        public ContactDto Contact { get; init; }

        public ICollection<OpeningDto> Openings { get; init; }
        
        public ICollection<UserDto> Employees { get; init; }
        
        public GetShop.Models.UserDto User { get; init; }
        
        public DateTime Creation { get; init; }
    }
}