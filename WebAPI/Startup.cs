using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.AzureSpeechAnalysis;
using WebAPI.SGCityServices;
using WebAPI.SGCityServicesClient;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", 
                    new OpenApiInfo 
                    { 
                        Title = "VadianVibes", 
                        Version = "v1",
                        Description = "Open API of VadianVibes",
                        Contact = new OpenApiContact
                        {
                            Name = "David Eggenberger"
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Example License",
                            Url = new Uri("https://example.com/license")
                        }
                    });
            });
            services.AddHttpClient<CityServiceAPIClient>(options =>
            {
                options.BaseAddress = new Uri("https://daten.stadt.sg.ch/api/records/1.0/search/?dataset=uebersicht-dienstleistungen-stadt-st-gallen-arbeitsversion&q=&rows=5000&sort=thema&facet=thema&facet=art_der_dienstleistung&facet=dienststelle&facet=dienststelle_name&facet=direktion_name&facet=direktion_kurzbezeichnung");
            });
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddSingleton<InMemoryCityServicesCollection>();
            services.Configure<AzureSpeechAnalysisOptions>(options =>
            {
                //options.Endpoint = Configuration["AzureSpeechAnalysisEndpoint"];
                options.APIKey = Configuration["AzureSpeechAnalysisAPIKey"];
            });
            services.AddHttpClient<AzureSpeechAnalysisAPIClient>(options =>
            {
                options.BaseAddress = new Uri("https://switzerlandnorth.api.cognitive.microsoft.com/sts/v1.0/");
            });
            services.AddScoped<SGCityServiceSearchService>();
            services.AddScoped<AzureSpeechToTextService>();
            services.AddHttpClient("default");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options => 
            {
                options.InjectStylesheet("/swagerStyles.css");
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
