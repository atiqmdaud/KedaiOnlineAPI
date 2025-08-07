using AutoMapper;
using KedaiOnline.Application.Items.Dtos;
using KedaiOnline.Domain.Entities;
using KedaiOnline.Domain.Exceptions;
using KedaiOnline.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KedaiOnline.Application.Items.Queries.GetItem;

public class GetItemQueryHandler(ILogger<GetItemQueryHandler> logger,
    IKedaiOnlineRepository kedaiOnlineRepository,
    IMapper mapper) : IRequestHandler<GetItemQuery, ItemDto>
{
    public async Task<ItemDto> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving Item : {ItemId} for Kedai id: {KedaiId}",request.ItemId, request.KedaiId);
        var kedai = await kedaiOnlineRepository.GetByIdAsync(request.KedaiId) ?? throw new NotFoundException(nameof(Kedai), request.KedaiId.ToString());

        var item = kedai.Items.FirstOrDefault(i => i.Id == request.ItemId) ?? throw new NotFoundException(nameof(Item), request.ItemId.ToString());

        var result = mapper.Map<ItemDto>(item);
        return result;
    }
}
