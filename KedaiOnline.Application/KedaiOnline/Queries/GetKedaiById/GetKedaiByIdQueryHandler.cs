using AutoMapper;
using KedaiOnline.Application.KedaiOnline.Dtos;
using KedaiOnline.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KedaiOnline.Application.KedaiOnline.Queries.GetKedaiById;

public class GetKedaiByIdQueryHandler(ILogger<GetKedaiByIdQueryHandler> logger,
    IMapper mapper, IKedaiOnlineRepository kedaiOnlineRepository) : IRequestHandler<GetKedaiByIdQuery, KedaiDto?>
{
    public async Task<KedaiDto?> Handle(GetKedaiByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching Kedai Online {KedaiId}",request.Id);
        var kedai = await kedaiOnlineRepository.GetByIdAsync(request.Id);
        //var kedaiDto = kedai != null ? KedaiDto.FromEntity(kedai) : null;
        //var kedaiDto = KedaiDto.FromEntity(kedai);
        var kedaiDto = mapper.Map<KedaiDto?>(kedai);
        return kedaiDto;
    }
}
