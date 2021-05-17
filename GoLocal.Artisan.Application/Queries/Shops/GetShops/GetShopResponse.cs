using System;
using System.Collections.Generic;
using GoLocal.Artisan.Application.Queries.Shops.GetShops.Models;

namespace GoLocal.Artisan.Application.Queries.Shops.GetShops
{
    public class ShopDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        
        public LocalisationDto Localisation { get; init; }
        public ContactDto Contact { get; init; }

        public ICollection<OpeningDto> Openings { get; init; }
        
        public UserDto User { get; init; }
        
        public DateTime Creation { get; init; }
    }
}