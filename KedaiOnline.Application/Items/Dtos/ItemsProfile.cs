using AutoMapper;
using KedaiOnline.Domain.Entities;

namespace KedaiOnline.Application.Items.Dtos;

public class ItemsProfile : Profile
{
    public ItemsProfile()
    {
        CreateMap<Item, ItemDto>();//matches all properties by name
    }
}
