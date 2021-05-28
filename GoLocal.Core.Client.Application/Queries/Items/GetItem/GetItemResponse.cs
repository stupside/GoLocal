using System;
using System.Collections.Generic;
using System.Linq;
using GoLocal.Core.Client.Application.Queries.Items.GetItem.Models;

namespace GoLocal.Core.Client.Application.Queries.Items.GetItem
{
    public class GetItemResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public bool Hidden { get; init; }
        public bool Available { get; init; }
        public DateTime Creation { get; init; }

        public double Rate => Rates.Average();
        public double RateMin => Rates.Min();
        public double RateMax => Rates.Max();
        private IEnumerable<int> Rates => Comments.Select(m => m.Rate).DefaultIfEmpty();
        
        public double PriceMax => Prices.Max();
        public double PriceMin => Prices.Min();
        public double PriceAverage => Prices.Average();
        private IEnumerable<float> Prices => Packages.Select(m => m.Price).DefaultIfEmpty();
        
        public HashSet<CommentDto> Comments { get; init; }
        public HashSet<PackageDto> Packages { get; init; }
        
        public string Image { get; init; }
    }
}