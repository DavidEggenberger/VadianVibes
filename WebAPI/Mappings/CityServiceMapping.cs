using AutoMapper;
using DTOs;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Domain;

namespace WebAPI.Mappings
{
    public class CityServiceMapping : Profile
    {
        public CityServiceMapping()
        {
            CreateMap<CityService, CityServiceDTO>();
            CreateMap<IEnumerable<IGrouping<string, CityService>>, IEnumerable<IGrouping<string, CityServiceDTO>>>();
        }
    }
}
