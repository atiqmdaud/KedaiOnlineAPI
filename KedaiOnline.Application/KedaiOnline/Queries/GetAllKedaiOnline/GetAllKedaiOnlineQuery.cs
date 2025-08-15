using KedaiOnline.Application.Common;
using KedaiOnline.Application.KedaiOnline.Dtos;
using KedaiOnline.Domain.Constants;
using MediatR;

namespace KedaiOnline.Application.KedaiOnline.Queries.GetAllKedaiOnline;

public class GetAllKedaiOnlineQuery : IRequest<PagedResult<KedaiDto>>
{
    public string? SearchTerm { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
