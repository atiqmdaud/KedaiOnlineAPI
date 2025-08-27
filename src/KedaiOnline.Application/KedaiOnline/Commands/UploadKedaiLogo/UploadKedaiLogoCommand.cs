using MediatR;

namespace KedaiOnline.Application.KedaiOnline.Commands.UploadKedaiLogo;

public class UploadKedaiLogoCommand : IRequest
{
    public int KedaiId { get; set; }
    public string FileName { get; set; } = default!;
    public Stream File { get; set; } = default!;
}
