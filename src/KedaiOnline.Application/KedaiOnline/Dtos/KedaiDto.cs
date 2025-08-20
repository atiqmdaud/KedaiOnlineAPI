using KedaiOnline.Application.Items.Dtos;
using KedaiOnline.Domain.Entities;

namespace KedaiOnline.Application.KedaiOnline.Dtos;

public class KedaiDto
{
    public int Id { get; set; }
    public string Nama { get; set; } = default!; // Default value to avoid null reference
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public string HasDelivery { get; set; } = default!;
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public List<ItemDto> Items { get; set; } = [];

    //public static KedaiDto? FromEntity(Kedai? kedai)
    //{
    //    if (kedai == null) return null;

    //    return new KedaiDto
    //    {
    //        Id = kedai.Id,
    //        Nama = kedai.Nama,
    //        Description = kedai.Description,
    //        Category = kedai.Category,
    //        HasDelivery = kedai.HasDelivery,
    //        City = kedai.Address?.City,
    //        Street = kedai.Address?.Street,
    //        PostalCode = kedai.Address?.PostalCode,
    //        //Items = kedai.Items.Select(ItemDto.FromEntity).ToList()
    //        Items = kedai.Items.Select(ItemDto.FromEntity).ToList()
    //    };
    //}

}
