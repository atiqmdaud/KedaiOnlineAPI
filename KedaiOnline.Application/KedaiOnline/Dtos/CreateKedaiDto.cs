using System.ComponentModel.DataAnnotations;

namespace KedaiOnline.Application.KedaiOnline.Dtos;

public class CreateKedaiDto
{
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Nama cannot be shorter than 5 characters.")]
    public string Nama { get; set; } = default!;
    public string Description { get; set; } = default!;
    [Required(ErrorMessage = "Category is required.")]
    public string Category { get; set; } = default!;
    public string HasDelivery { get; set; } = default!;

    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string? ContactEmail { get; set; }
    [Phone(ErrorMessage = "Invalid phone number format.")]
    public string? ContactNumber { get; set; }

    public string? City { get; set; }
    public string? Street { get; set; }
    [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Please provide valid postal code (XX-XXX)")]
    public string? PostalCode { get; set; }
}
