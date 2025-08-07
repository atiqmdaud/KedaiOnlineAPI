using KedaiOnline.Application.Items.Dtos;
using MediatR;

namespace KedaiOnline.Application.Items.Queries.GetItem;

public class GetItemQuery(int kedaiId, int itemId) : IRequest<ItemDto>
{
    public int KedaiId { get; } = kedaiId;
    public int ItemId { get; } = itemId;
}
