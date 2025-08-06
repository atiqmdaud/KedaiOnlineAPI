using KedaiOnline.Domain.Entities;
using KedaiOnline.Domain.Exceptions;
using KedaiOnline.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KedaiOnline.Application.KedaiOnline.Commands.DeleteKedai;

public class DeleteKedaiCommandHandler(ILogger<DeleteKedaiCommandHandler> logger,
    IKedaiOnlineRepository kedaiOnlineRepository) : IRequestHandler<DeleteKedaiCommand>
{
    public async Task Handle(DeleteKedaiCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting Kedai with ID: {KedaiId}", request.Id);
        var kedai = await kedaiOnlineRepository.GetByIdAsync(request.Id);
        if (kedai is null)
        {
            throw new NotFoundException(nameof(Kedai), request.Id.ToString());
        }

        await kedaiOnlineRepository.DeleteAsync(kedai);
        logger.LogInformation("Kedai with ID: {Id} deleted successfully", request.Id);

    }
}
