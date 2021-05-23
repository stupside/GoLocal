using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using GoLocal.Shared.Locate.Configuration;
using GoLocal.Shared.Locate.Interfaces;
using GoLocal.Shared.Locate.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GoLocal.Shared.Locate.Implementations
{
    public class LocateService : ILocateService
    {
        private readonly LocateConfiguration _configuration;
        private readonly HttpClient _httpClient;
        
        public LocateService(IOptions<LocateConfiguration> configuration)
        {
            _configuration = configuration.Value;
            _httpClient = new HttpClient();
        }

        public async Task<Place> GetPosition(string query, int limit = 1)
        {
            string url = $"{_configuration.Url}/mapbox.places/{HttpUtility.HtmlEncode(query)}.json?access_token={_configuration.Token}&limit={limit}";
            
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result= response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Place>(result);
            }
            return null;
        }
    }
}