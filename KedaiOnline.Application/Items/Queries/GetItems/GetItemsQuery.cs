using KedaiOnline.Application.Items.Dtos;
using MediatR;

namespace KedaiOnline.Application.Items.Queries.GetItems;

public class GetItemsQuery(int kedaiId) : IRequest<IEnumerable<ItemDto>>
{
    public int KedaiId { get; } = kedaiId;

}
