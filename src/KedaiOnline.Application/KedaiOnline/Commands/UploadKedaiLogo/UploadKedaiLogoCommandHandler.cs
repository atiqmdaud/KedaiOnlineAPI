using KedaiOnline.Domain.Constants;
using KedaiOnline.Domain.Entities;
using KedaiOnline.Domain.Exceptions;
using KedaiOnline.Domain.Interfaces;
using KedaiOnline.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KedaiOnline.Application.KedaiOnline.Commands.UploadKedaiLogo;

internal class UploadKedaiLogoCommandHandler(ILogger<UploadKedaiLogoCommandHandler> logger,
    IKedaiOnlineRepository kedaiOnlineRepository,
    IKedaiAuthorizationService kedaiAuthorizationService,
    IBlobStorageService blobStorageService) : IRequestHandler<UploadKedaiLogoCommand>
{
    public async Task Handle(UploadKedaiLogoCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Uploading logo for kedai id: {KedaiId}", request.KedaiId);

        var kedai = await kedaiOnlineRepository.GetByIdAsync(request.KedaiId);
        if (kedai is null)
        {
            throw new NotFoundException(nameof(Kedai), request.KedaiId.ToString());
        }

        if (!kedaiAuthorizationService.Authorize(kedai, ResourceOperation.Update))
        {
            throw new ForbidException();
        }

       var logoUrl = await blobStorageService.UploadToBlobAsync(request.File, request.FileName);
        
       kedai.LogoUrl = logoUrl;

       await kedaiOnlineRepository.SaveChangesAsync();
    }
}
