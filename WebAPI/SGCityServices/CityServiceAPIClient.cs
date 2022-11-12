using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI.Domain;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Linq;
using System;
using System.Text;
using IronPdf;

namespace WebAPI.SGCityServicesClient
{
    public class CityServiceAPIClient
    {
        private readonly HttpClient httpClient;
        private readonly IHttpClientFactory httpClientFactory;
        public CityServiceAPIClient(HttpClient httpClient, IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClient;
            this.httpClientFactory = httpClientFactory; 
        }

        public async Task<IEnumerable<CityService>> LoadAllCityServicesFromAPI()
        {
            var t = await httpClient.GetFromJsonAsync<APIResponseRoot>("");
            return t.records.Select(r => r.fields);
        }

        public async Task ExtendServicesWithScrapedInformations(List<CityService> cityServices)
        {
            var defaultHttpClient = httpClientFactory.CreateClient("default");
            foreach (var service in cityServices)
            {
                if(string.IsNullOrWhiteSpace(service.merkblatt_link) is false)
                {
                    try
                    {
                        var pdf = PdfDocument.FromUrl(new Uri(service.merkblatt_link));
                        string AllText = pdf.ExtractAllText();
                        service.ScrapedInformation.ScrapedInformationFromLinkedWebsite = AllText;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                if(string.IsNullOrWhiteSpace(service.direktlink_url) is false)
                {
                    try
                    {
                        service.ScrapedInformation.ScrapedInformationFromLinkedWebsite = await defaultHttpClient.GetStringAsync(service.direktlink_url);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}
