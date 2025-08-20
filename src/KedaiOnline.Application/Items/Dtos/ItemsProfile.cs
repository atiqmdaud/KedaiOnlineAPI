using AutoMapper;
using KedaiOnline.Application.Items.Commands.CreateItem;
using KedaiOnline.Domain.Entities;

namespace KedaiOnline.Application.Items.Dtos;

public class ItemsProfile : Profile
{
    public ItemsProfile()
    {
        CreateMap<CreateItemCommand, Item>();
        CreateMap<Item, ItemDto>();//matches all properties by name
    }
}
