using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.MappingProfiles
{
    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            CreateMap<Platform, PlatformReadDto>()
                .ForMember(dest => dest.PlatformName, 
                                act => act.MapFrom(src => src.Name));
            CreateMap<PlatformCreateDto, Platform>();
        }
    }
}
