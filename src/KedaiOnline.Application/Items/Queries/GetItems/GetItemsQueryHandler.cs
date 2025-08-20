using AutoMapper;
using KedaiOnline.Application.Items.Dtos;
using KedaiOnline.Domain.Entities;
using KedaiOnline.Domain.Exceptions;
using KedaiOnline.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KedaiOnline.Application.Items.Queries.GetItems;

public class GetItemsQueryHandler(ILogger<GetItemsQueryHandler> logger,
    IKedaiOnlineRepository kedaiOnlineRepository,
    IMapper mapper) : IRequestHandler<GetItemsQuery, IEnumerable<ItemDto>>
{
    public async Task<IEnumerable<ItemDto>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving Items for KedaiId: {KedaiId}", request.KedaiId);
        var kedai = await kedaiOnlineRepository.GetByIdAsync(request.KedaiId) ?? throw new NotFoundException(nameof(Kedai), request.KedaiId.ToString());

        var items = mapper.Map<IEnumerable<ItemDto>>(kedai.Items);

        return items;
    }
}
