using MediatR;

namespace KedaiOnline.Application.Users.Comands.AssignUserRole;

public class AssignUserRoleCommand : IRequest
{
    public string UserEmail { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}
