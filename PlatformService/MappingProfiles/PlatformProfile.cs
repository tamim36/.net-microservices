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
            CreateMap<PlatformReadDto, PlatformPublishedDto>()
                .ForMember(dest => dest.Name,
                                act => act.MapFrom(src => src.PlatformName));
            CreateMap<Platform, GrpcPlatformModel>()
                .ForMember(dest => dest.PlatformId,
                                act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,
                                act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.Publisher,
                                act => act.MapFrom(src => src.Publisher));
        }
    }
}
