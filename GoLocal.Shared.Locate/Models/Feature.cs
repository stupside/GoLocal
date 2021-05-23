using System.Collections.Generic;
using Newtonsoft.Json;

namespace GoLocal.Shared.Locate.Models
{
    public class Feature
    {
        [JsonProperty("id")]
        public string Id { get; init; }
        
        [JsonProperty("place_name")]
        public string PlaceName { get; init; }
        
        [JsonProperty("center")]
        public List<double> Coordinates { get; init; }
        
        [JsonProperty("context")]
        public List<IDictionary<string, string>> Contexts { get; init; }
        
        [JsonProperty("text")]
        public string Street { get; init; }

        public double Longitude => Coordinates[0];
        public double Latitude => Coordinates[1];
    }
}