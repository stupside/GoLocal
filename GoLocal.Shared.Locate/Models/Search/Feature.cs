using System.Collections.Generic;
using System.Linq;
using GoLocal.Shared.Locate.Models.Search.Constants;
using Newtonsoft.Json;

namespace GoLocal.Shared.Locate.Models.Search
{
    public class Feature
    {
        [JsonProperty("id")]
        public string Id { get; init; }
        
        [JsonProperty("relevance")]
        public string Relevance { get; init; }
        [JsonProperty("place_name")]
        public string PlaceName { get; init; }
        [JsonProperty("matching_text")]
        public string MatchingText { get; init; }
        [JsonProperty("matching_place_name")]
        public string MatchingPlaceName { get; init; }

        [JsonProperty("text")]
        public string Street { get; init; }
        [JsonProperty("address")]
        public string Address { get; init; }
        
        
        [JsonProperty("context")]
        public List<Context> Contexts { private get; init; }
        public string PostCode => Contexts.SingleOrDefault(m => m.Id.Contains(Features.PostCode))?.Text;
        public string Country => Contexts.SingleOrDefault(m => m.Id.Contains(Features.Country))?.Text;
        public string Region => Contexts.SingleOrDefault(m => m.Id.Contains(Features.Region))?.Text;
        public string City => Contexts.SingleOrDefault(m => m.Id.Contains(Features.Place))?.Text;
        public string NeighborHood => Contexts.SingleOrDefault(m => m.Id.Contains(Features.NeighborHood))?.Text;
        
        
        [JsonProperty("center")]
        public List<double> Coordinates { private get; init; }
        public double Longitude => Coordinates[0];
        public double Latitude => Coordinates[1];

        public bool IsValid => Relevance == "1" && PostCode != Country && Country != Region && Region != City && !string.IsNullOrEmpty(PostCode);
    }
}