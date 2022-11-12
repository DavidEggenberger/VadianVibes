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
            //await cityServiceAPIClient.ExtendServicesWithScrapedInformations(inMemoryCollection.CityServices);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext) =>
                {
                    hostingContext.AddAzureKeyVault(new Uri("https://vadianvibes.vault.azure.net/"),
                            new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = "df592a24-6942-4b26-bd17-070fe244a43c" }));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
