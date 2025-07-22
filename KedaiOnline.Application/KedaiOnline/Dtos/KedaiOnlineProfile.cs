using AutoMapper;
using KedaiOnline.Application.KedaiOnline.Commands.CreateKedai;
using KedaiOnline.Application.KedaiOnline.Commands.UpdateKedai;
using KedaiOnline.Domain.Entities;

namespace KedaiOnline.Application.KedaiOnline.Dtos;

public class KedaiOnlineProfile : Profile
{
    public KedaiOnlineProfile()
    {
        CreateMap<UpdateKedaiCommand, Kedai>();

        CreateMap<CreateKedaiCommand, Kedai>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
            {
                City = src.City,
                Street = src.Street,
                PostalCode = src.PostalCode
            }));
            //.ForMember(dest => dest.Items, opt => opt.Ignore()); // Assuming Items will be set separately

        CreateMap<Kedai, KedaiDto>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
    }
}
