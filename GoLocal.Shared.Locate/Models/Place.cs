using System.Collections.Generic;

namespace GoLocal.Shared.Locate.Models
{
    public class Place
    {
        public IEnumerable<Feature> Features { get; init; }
        
        public IEnumerable<string> Queries { get; init; }
        
    }
}