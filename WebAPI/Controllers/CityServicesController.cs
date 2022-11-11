using AutoMapper;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public CityServicesController(ILogger<CityServicesController> logger, CityServiceAPIClient cityServiceAPIClient, IMapper mapper)
        {
            _logger = logger;
            this.cityServiceAPIClient = cityServiceAPIClient;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CityServiceDTO>> GetCityServicesAsync()
        {
            var cityServices = await cityServiceAPIClient.LoadAllCityServicesFromAPI();
            return mapper.Map<IEnumerable<CityServiceDTO>>(cityServices);
        }
    }
}
