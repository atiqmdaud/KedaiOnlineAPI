using Microsoft.AspNetCore.Identity;

namespace KedaiOnline.Domain.Entities;

public class User : IdentityUser
{
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }

    public List<Kedai> OwnedKedaiOnline { get; set; } = [];
}
