using Newtonsoft.Json;

namespace GoLocal.Shared.Locate.Models.Search
{
    public class Context
    {
        [JsonProperty("id")]
        public string Id { get; init; }
        [JsonProperty("text")]
        public string Text { get; init; }
        [JsonProperty("wikidata")]
        public string WikiData { get; init; }
        [JsonProperty("short_code")]
        public string ShortCode { get; init; }
    }
}