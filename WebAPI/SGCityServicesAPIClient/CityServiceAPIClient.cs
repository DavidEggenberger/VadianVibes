using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI.Domain;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Linq;

namespace WebAPI.SGCityServicesClient
{
    public class CityServiceAPIClient
    {
        private readonly HttpClient httpClient;
        public CityServiceAPIClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<CityService>> LoadAllCityServicesFromAPI()
        {
            var t = await httpClient.GetFromJsonAsync<APIResponseRoot>("");
            return t.records.Select(r => r.fields);
        }
    }
}
