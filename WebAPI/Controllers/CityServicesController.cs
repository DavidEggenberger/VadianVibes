﻿using AutoMapper;
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
        private readonly InMemoryCityServicesCollection inMemoryCityServicesCollection;
        public CityServicesController(ILogger<CityServicesController> logger, CityServiceAPIClient cityServiceAPIClient, IMapper mapper, InMemoryCityServicesCollection inMemoryCityServicesCollection)
        {
            _logger = logger;
            this.cityServiceAPIClient = cityServiceAPIClient;
            this.mapper = mapper;
            this.inMemoryCityServicesCollection = inMemoryCityServicesCollection;
        }

        [HttpGet]
        public IEnumerable<CityServiceDTO> GetCityServicesAsync()
        {
            return mapper.Map<IEnumerable<CityServiceDTO>>(inMemoryCityServicesCollection.CityServices);
        }
    }
}