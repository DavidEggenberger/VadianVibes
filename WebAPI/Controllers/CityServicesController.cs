using AutoMapper;
using DTOs;
using DTOs.CityService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;
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
        private readonly AzureSpeechToTextService azureSpeechToTextService;
        public CityServicesController(IMapper mapper, InMemoryCityServicesCollection inMemoryCityServicesCollection, SGCityServiceSearchService cityServiceSearchService, AzureSpeechToTextService azureSpeechToTextService)
        {
            this.mapper = mapper;
            this.inMemoryCityServicesCollection = inMemoryCityServicesCollection;
            this.cityServiceSearchService = cityServiceSearchService;
            this.azureSpeechToTextService = azureSpeechToTextService;
        }

        [HttpGet]
        public IEnumerable<CityServiceDTO> GetSearchedCityServices(
            [FromQuery] IEnumerable<string> keywords, 
            [FromQuery] KeywordSearchOption? keywordSearchOption,
            [FromQuery] SearchInLinkedDocumentSearchOption? searchInLinkedDocumentSearchOption)
        {
            var services = cityServiceSearchService.SearchCityServices(keywords, keywordSearchOption, searchInLinkedDocumentSearchOption);
            return mapper.Map<IEnumerable<CityServiceDTO>>(services);
        }

        [HttpGet("groupedBy")]
        public Dictionary<string, List<CityServiceDTO>> GetGroupedCityServicesGroupedAsync(
            [FromQuery] IEnumerable<string> keywords,
            [FromQuery] KeywordSearchOption? keywordSearchOption,
            [FromQuery] SearchInLinkedDocumentSearchOption? searchInLinkedDocumentSearchOption,
            [FromQuery] CityServiceGroupBy groupBy)
        {
            var services = cityServiceSearchService.SearchCityServices(keywords, keywordSearchOption, searchInLinkedDocumentSearchOption);
            var servicesDTOs = mapper.Map<IEnumerable<CityServiceDTO>>(services);

            var grouped = groupBy switch
            {
                CityServiceGroupBy.art_der_dienstleistung => servicesDTOs.GroupBy(s => s.art_der_dienstleistung),
                CityServiceGroupBy.dienststelle => servicesDTOs.GroupBy(s => s.dienststelle),
                CityServiceGroupBy.thema => servicesDTOs.GroupBy(s => s.thema),
                _ => servicesDTOs.GroupBy(s => s.art_der_dienstleistung)
            };

            return grouped.Where(g => g.Key != null && g.Count() > 0).ToDictionary(kvp => $"{groupBy}: {kvp.Key}", kvp => kvp.ToList());
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<CityServiceDTO>>> PostWavFile(IFormFile wavFile)
        {
            if(wavFile == null || wavFile.ContentType != "audio/wav")
            {
                return BadRequest("Please provide a .wav File");
            }

            var foundKeywords = await azureSpeechToTextService.AnalyzeFormFile(wavFile);

            return null;
        }
    }
}
