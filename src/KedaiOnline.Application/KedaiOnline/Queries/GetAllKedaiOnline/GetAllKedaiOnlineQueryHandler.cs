using AutoMapper;
using KedaiOnline.Application.Common;
using KedaiOnline.Application.KedaiOnline.Dtos;
using KedaiOnline.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KedaiOnline.Application.KedaiOnline.Queries.GetAllKedaiOnline;

public class GetAllKedaiOnlineQueryHandler(ILogger<GetAllKedaiOnlineQueryHandler> logger,
    IMapper mapper,IKedaiOnlineRepository kedaiOnlineRepository) : IRequestHandler<GetAllKedaiOnlineQuery, PagedResult<KedaiDto>>
{
    public async Task<PagedResult<KedaiDto>> Handle(GetAllKedaiOnlineQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching all Kedai Online");
        var (kedaiOnline, totalCount) = await kedaiOnlineRepository.GetAllMatchingAsync(request.SearchTerm,
            request.PageSize,
            request.PageNumber,
            request.SortBy,
            request.SortDirection);
        //var kedaiDtos = kedaiOnline.Select(KedaiDto.FromEntity);
        var kedaiDtos = mapper.Map<IEnumerable<KedaiDto>>(kedaiOnline);

        var result = new PagedResult<KedaiDto>(kedaiDtos,totalCount,request.PageNumber,request.PageSize);

        return result;
    }
}
