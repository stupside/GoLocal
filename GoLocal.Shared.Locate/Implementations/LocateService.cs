using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using GoLocal.Shared.Locate.Configuration;
using GoLocal.Shared.Locate.Interfaces;
using GoLocal.Shared.Locate.Models.Search;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GoLocal.Shared.Locate.Implementations
{
    public class LocateService : ILocateService
    {
        private readonly LocateConfiguration _configuration;
        private readonly HttpClient _client;
        
        public LocateService(IOptions<LocateConfiguration> configuration)
        {
            _configuration = configuration.Value;

            _client = new HttpClient
            {
                BaseAddress = new Uri(_configuration.Url)
            };
        }

        public async Task<Place> GetPosition(params string[] query)
        {
            string url = $"mapbox.places/{HttpUtility.UrlEncode(string.Join(' ', query))}.json?access_token={_configuration.Token}&limit={1}";
            
            var response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            string body = await response.Content.ReadAsStringAsync();
            
            Place place = JsonConvert.DeserializeObject<Place>(body);
            
            return place;
        }
    }
}