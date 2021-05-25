using System.Collections.Generic;
using System.Linq;

namespace GoLocal.Shared.Locate.Models.Search
{
    public class Place
    {
        public IEnumerable<Feature> Features { get; init; }
        
        public IEnumerable<string> Queries { get; init; }
        public bool Any => Features.Any();
        public Feature Feature => Features.FirstOrDefault();
    }
}