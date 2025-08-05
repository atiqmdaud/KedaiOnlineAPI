using KedaiOnline.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KedaiOnline.Application.KedaiOnline.Commands.DeleteKedai;

public class DeleteKedaiCommandHandler(ILogger<DeleteKedaiCommandHandler> logger,
    IKedaiOnlineRepository kedaiOnlineRepository) : IRequestHandler<DeleteKedaiCommand, bool>
{
    public async Task<bool> Handle(DeleteKedaiCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting Kedai with ID: {KedaiId}", request.Id);
        var kedai = await kedaiOnlineRepository.GetByIdAsync(request.Id);
        if (kedai == null)
        {
            logger.LogWarning("Kedai with ID: {Id} not found", request.Id);
            return false;
        }

        await kedaiOnlineRepository.DeleteAsync(kedai);
        logger.LogInformation("Kedai with ID: {Id} deleted successfully", request.Id);
        return true;

    }
}
