using System;
using System.Collections.Generic;
using GoLocal.Artisan.Application.Queries.Items.GetItem.Models;

namespace GoLocal.Artisan.Application.Queries.Items.GetItem
{
    public class GetItemResponse
    {
        public int Id { get; init; }
        
        public string Name { get; init; }
        public string Description { get; init; }
        
        public bool Hidden { get; init; }
        public bool Available { get; init; }
        
        public ICollection<PackageDto> Packages { get; init; }
        
        public DateTime Creation { get; init; }
    }
}