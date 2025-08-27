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
    public string? LogoSasUrl { get; set; }
    public List<ItemDto> Items { get; set; } = [];


}
