using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.SGCityServicesClient;

namespace WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var inMemoryCollection = host.Services.GetRequiredService<InMemoryCityServicesCollection>();
            var cityServiceAPIClient = host.Services.GetRequiredService<CityServiceAPIClient>();
            inMemoryCollection.CityServices = (await cityServiceAPIClient.LoadAllCityServicesFromAPI()).ToList();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext) =>
                {
                    hostingContext.AddAzureKeyVault(new Uri("https://starthackmkeyvault.vault.azure.net/"),
                            new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = "fa1648c8-2088-4c7f-ab95-187a90f17ccc" }));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
