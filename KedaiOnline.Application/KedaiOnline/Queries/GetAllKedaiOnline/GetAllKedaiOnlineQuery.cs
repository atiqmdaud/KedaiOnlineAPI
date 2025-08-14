using KedaiOnline.Application.KedaiOnline.Dtos;
using MediatR;

namespace KedaiOnline.Application.KedaiOnline.Queries.GetAllKedaiOnline;

public class GetAllKedaiOnlineQuery : IRequest<IEnumerable<KedaiDto>>
{
    public string? SearchTerm { get; set; }
}
