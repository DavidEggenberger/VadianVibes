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
using WebAPI.SGCityServices;
using WebAPI.SGCityServicesClient;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityServicesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly InMemoryCityServicesCollection inMemoryCityServicesCollection;
        private readonly SGCityServiceSearchService cityServiceSearchService;
        public CityServicesController(IMapper mapper, InMemoryCityServicesCollection inMemoryCityServicesCollection, SGCityServiceSearchService cityServiceSearchService)
        {
            this.mapper = mapper;
            this.inMemoryCityServicesCollection = inMemoryCityServicesCollection;
            this.cityServiceSearchService = cityServiceSearchService;
        }

        [HttpGet]
        public async Task<IEnumerable<CityServiceDTO>> GetSearchedCityServices(
            [FromQuery] IEnumerable<string> keywords, 
            [FromQuery] KeywordSearchOption? keywordSearchOption,
            [FromQuery] SearchInLinkedDocumentSearchOption? searchInLinkedDocumentSearchOption,
            [FromQuery] string description)
        {
            var services = cityServiceSearchService.SearchCityServices(keywords, keywordSearchOption, searchInLinkedDocumentSearchOption, description);
            return mapper.Map<IEnumerable<CityServiceDTO>>(services);
        }

        [HttpGet("grouped")]
        public IEnumerable<CityServiceDTO> GetCityServicesGroupedAsync()
        {
            return mapper.Map<IEnumerable<CityServiceDTO>>(inMemoryCityServicesCollection.CityServices);
        }
    }
}
