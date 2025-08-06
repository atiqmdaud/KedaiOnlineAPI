using MediatR;

namespace KedaiOnline.Application.KedaiOnline.Commands.UpdateKedai;

public class UpdateKedaiCommand : IRequest
{
    public int Id { get; set; }
    public string Nama { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string HasDelivery { get; set; } = default!;
}
