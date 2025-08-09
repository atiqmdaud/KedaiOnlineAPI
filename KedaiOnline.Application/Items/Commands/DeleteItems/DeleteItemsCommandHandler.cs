using KedaiOnline.Domain.Entities;
using KedaiOnline.Domain.Exceptions;
using KedaiOnline.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KedaiOnline.Application.Items.Commands.DeleteItems;

public class DeleteItemsCommandHandler(ILogger<DeleteItemsCommandHandler> logger,
    IKedaiOnlineRepository kedaiOnlineRepository,
    IItemsRepository itemsRepository) : IRequestHandler<DeleteItemsCommand>
{
    public async Task Handle(DeleteItemsCommand request, CancellationToken cancellationToken)
    {
        logger.LogWarning("Deleting items for kedai with ID {KedaiId}", request.KedaiId);
        var kedaiOnline = await kedaiOnlineRepository.GetByIdAsync(request.KedaiId) ?? throw new NotFoundException(nameof(Kedai), request.KedaiId.ToString());
        await itemsRepository.DeleteItems(kedaiOnline.Items);
    }
}
