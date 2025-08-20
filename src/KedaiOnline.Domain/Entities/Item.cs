namespace KedaiOnline.Domain.Entities;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = default!; // Default value to avoid null reference
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }

    public int KedaiId { get; set; } // Foreign key to Kedai //must same data type as Kedai.Id
}
