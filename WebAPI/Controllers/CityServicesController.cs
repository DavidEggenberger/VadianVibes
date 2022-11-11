using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityServicesController : ControllerBase
    {
        private readonly ILogger<CityServicesController> _logger;

        public CityServicesController(ILogger<CityServicesController> logger)
        {
            _logger = logger;
        }

        
    }
}
