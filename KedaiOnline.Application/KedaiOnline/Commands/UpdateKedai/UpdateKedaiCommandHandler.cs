using AutoMapper;
using KedaiOnline.Application.KedaiOnline.Commands.DeleteKedai;
using KedaiOnline.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KedaiOnline.Application.KedaiOnline.Commands.UpdateKedai;

public class UpdateKedaiCommandHandler(ILogger<UpdateKedaiCommandHandler> logger,
    IKedaiOnlineRepository kedaiOnlineRepository, IMapper mapper) : IRequestHandler<UpdateKedaiCommand, bool>
{
    public async Task<bool> Handle(UpdateKedaiCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating Kedai with ID: {KedaiId} with {@UpdatedKedai}", request.Id,request);
        var kedai = await kedaiOnlineRepository.GetByIdAsync(request.Id);
        if (kedai == null)
        {
            logger.LogWarning("Kedai with ID: {Id} not found", request.Id);
            return false;
        }

        mapper.Map(request, kedai);

        //kedai.Nama = request.Nama;
        //kedai.Description = request.Description;
        //kedai.HasDelivery = request.HasDelivery;

        await kedaiOnlineRepository.SaveChangesAsync();
        logger.LogInformation("Kedai with ID: {Id} updated successfully", request.Id);
        return true;
    }

}
