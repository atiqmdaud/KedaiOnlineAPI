using AutoMapper;
using KedaiOnline.Application.KedaiOnline.Dtos;
using KedaiOnline.Domain.Entities;
using KedaiOnline.Domain.Exceptions;
using KedaiOnline.Domain.Interfaces;
using KedaiOnline.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KedaiOnline.Application.KedaiOnline.Queries.GetKedaiById;

public class GetKedaiByIdQueryHandler(ILogger<GetKedaiByIdQueryHandler> logger,
    IMapper mapper,
    IKedaiOnlineRepository kedaiOnlineRepository,
    IBlobStorageService blobStorageService) : IRequestHandler<GetKedaiByIdQuery, KedaiDto>
{
    public async Task<KedaiDto> Handle(GetKedaiByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching Kedai Online {KedaiId}",request.Id);
        var kedai = await kedaiOnlineRepository.GetByIdAsync(request.Id)
        ?? throw new NotFoundException(nameof(Kedai), request.Id.ToString());

        //var kedaiDto = kedai != null ? KedaiDto.FromEntity(kedai) : null;
        //var kedaiDto = KedaiDto.FromEntity(kedai);
        var kedaiDto = mapper.Map<KedaiDto>(kedai);

        kedaiDto.LogoSasUrl =  blobStorageService.GetBlobSasUrl(kedai.LogoUrl);

        return kedaiDto;
    }
}
