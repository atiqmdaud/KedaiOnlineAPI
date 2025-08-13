using AutoMapper;
using KedaiOnline.Domain.Constants;
using KedaiOnline.Domain.Entities;
using KedaiOnline.Domain.Exceptions;
using KedaiOnline.Domain.Interfaces;
using KedaiOnline.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KedaiOnline.Application.Items.Commands.CreateItem;

public class CreateItemCommandHandler(ILogger<CreateItemCommandHandler> logger,
    IKedaiOnlineRepository kedaiOnlineRepository,
    IItemsRepository itemsRepository,
    IMapper mapper,
    IKedaiAuthorizationService kedaiAuthorizationService) : IRequestHandler<CreateItemCommand, int>
{
    public async Task<int> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new item: {@ItemRequest}", request);
        var kedai = await kedaiOnlineRepository.GetByIdAsync(request.KedaiId);
        if (kedai is null)
        {
            throw new NotFoundException(nameof(Kedai), request.KedaiId.ToString());
        }

        if (!kedaiAuthorizationService.Authorize(kedai, ResourceOperation.Update))
        {
            throw new ForbidException();
        }

        var item = mapper.Map<Item>(request);

        return await itemsRepository.CreateAsync(item);
    }
}
