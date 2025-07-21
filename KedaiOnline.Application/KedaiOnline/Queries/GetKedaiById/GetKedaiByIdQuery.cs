using KedaiOnline.Application.KedaiOnline.Dtos;
using MediatR;

namespace KedaiOnline.Application.KedaiOnline.Queries.GetKedaiById;

public class GetKedaiByIdQuery(int id) : IRequest<KedaiDto?>
{
    public int Id { get; } = id;

}
