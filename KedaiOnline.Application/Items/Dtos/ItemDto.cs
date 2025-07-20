using KedaiOnline.Domain.Entities;

namespace KedaiOnline.Application.Items.Dtos;

public class ItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!; // Default value to avoid null reference
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }

    public static ItemDto FromEntity(Item item)
    {

        return new ItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Price = item.Price
        };
    }
}
