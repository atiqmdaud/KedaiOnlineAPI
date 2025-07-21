using MediatR;

namespace KedaiOnline.Application.KedaiOnline.Commands.CreateKedai;

public class CreateKedaiCommand : IRequest<int> //int represents the ID of the created Kedai
{
    public string Nama { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public string HasDelivery { get; set; } = default!;
    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
}
