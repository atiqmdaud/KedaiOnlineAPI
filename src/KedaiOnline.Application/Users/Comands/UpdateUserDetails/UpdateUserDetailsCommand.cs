using MediatR;

namespace KedaiOnline.Application.Users.Comands.UpdateUserDetails;

public class UpdateUserDetailsCommand : IRequest
{
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
}
