namespace KedaiOnline.Domain.Entities;

public class Kedai
{
    public int Id { get; set; }
    public string Nama { get; set; } = default!; // Default value to avoid null reference
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public string HasDelivery { get; set; } = default!;

    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }

    public Address? Address { get; set; }
    public List<Item> Items { get; set; } = new();

    public User Owner { get; set; } = default!;
    public string OwnerId { get; set; } = default!;





}
