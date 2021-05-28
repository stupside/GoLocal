using System;
using System.Collections.Generic;
using System.Linq;
using GoLocal.Core.Client.Application.Queries.Items.GetItem.Models;

namespace GoLocal.Core.Client.Application.Queries.Items.GetItem
{
    public class GetItemResponse
    {
        public int Id { get; init; }
        public string Image { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public bool Hidden { get; init; }
        public bool Available { get; init; }
        public ICollection<CommentDto> Comments { get; init; }
        public double Rate => Comments.Average(m => m.Rate);
        public double PriceMin => Packages.Min(m => m.Price);
        public double Min => Comments.Min(m => m.Rate);
        public double Max => Comments.Max(m => m.Rate);
        public double PriceMax => Packages.Max(m => m.Price);
        public double PriceAverage => Packages.Average(m => m.Price);
        public ICollection<PackageDto> Packages { get; init; }
        public DateTime Creation { get; init; }
    }
}