using MediatR;

namespace KedaiOnline.Application.KedaiOnline.Commands.DeleteKedai;

public class DeleteKedaiCommand(int id) : IRequest<bool>
{
    public int Id { get; } = id;
}
