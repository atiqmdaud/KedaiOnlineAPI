using AutoMapper;
using KedaiOnline.Domain.Entities;

namespace KedaiOnline.Application.KedaiOnline.Dtos;

public class KedaiOnlineProfile : Profile
{
    public KedaiOnlineProfile()
    {
        CreateMap<Kedai, KedaiDto>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
    }
}
