using AutoMapper;
using DTOs;
using DTOs.CityService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.AzureSpeechAnalysis;
using WebAPI.Domain;
using WebAPI.SGCityServicesClient;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityServicesController : ControllerBase
    {
        private readonly ILogger<CityServicesController> _logger;
        private readonly CityServiceAPIClient cityServiceAPIClient;
        private readonly IMapper mapper;
        private readonly InMemoryCityServicesCollection inMemoryCityServicesCollection;
        private readonly AzureSpeechAnalysisAPIClient azureSpeechAnalysisAPIClient;
        public CityServicesController(ILogger<CityServicesController> logger, CityServiceAPIClient cityServiceAPIClient, IMapper mapper, InMemoryCityServicesCollection inMemoryCityServicesCollection, AzureSpeechAnalysisAPIClient azureSpeechAnalysisAPIClient)
        {
            _logger = logger;
            this.cityServiceAPIClient = cityServiceAPIClient;
            this.mapper = mapper;
            this.inMemoryCityServicesCollection = inMemoryCityServicesCollection;
            this.azureSpeechAnalysisAPIClient = azureSpeechAnalysisAPIClient;
        }

        [HttpGet]
        public async Task<IEnumerable<CityServiceDTO>> GetSearchedCityServices(
            [FromQuery] IEnumerable<string> keywords, 
            [FromQuery] string description)
        {
            var st = await azureSpeechAnalysisAPIClient.GetTokenAsync();
            return mapper.Map<IEnumerable<CityServiceDTO>>(inMemoryCityServicesCollection.CityServices);
        }

        [HttpGet("grouped")]
        public IEnumerable<CityServiceDTO> GetCityServicesGroupedAsync()
        {
            return mapper.Map<IEnumerable<CityServiceDTO>>(inMemoryCityServicesCollection.CityServices);
        }
    }
}
