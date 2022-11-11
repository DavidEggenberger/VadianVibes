using AutoMapper;
using DTOs;
using WebAPI.Domain;

namespace WebAPI.Mappings
{
    public class CityServiceMapping : Profile
    {
        public CityServiceMapping()
        {
            CreateMap<CityService, CityServiceDTO>();
        }
    }
}
