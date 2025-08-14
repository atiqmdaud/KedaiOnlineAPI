using AutoMapper;
using KedaiOnline.Application.KedaiOnline.Dtos;
using KedaiOnline.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KedaiOnline.Application.KedaiOnline.Queries.GetAllKedaiOnline;

public class GetAllKedaiOnlineQueryHandler(ILogger<GetAllKedaiOnlineQueryHandler> logger,
    IMapper mapper,IKedaiOnlineRepository kedaiOnlineRepository) : IRequestHandler<GetAllKedaiOnlineQuery, IEnumerable<KedaiDto>>
{
    public async Task<IEnumerable<KedaiDto>> Handle(GetAllKedaiOnlineQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching all Kedai Online");
        var kedaiOnline = await kedaiOnlineRepository.GetAllMatchingAsync(request.SearchTerm);
        //var kedaiDtos = kedaiOnline.Select(KedaiDto.FromEntity);
        var kedaiDtos = mapper.Map<IEnumerable<KedaiDto>>(kedaiOnline);
        return kedaiDtos!;// Ensure non-null return type mybe empty list if no kedai found
    }
}
