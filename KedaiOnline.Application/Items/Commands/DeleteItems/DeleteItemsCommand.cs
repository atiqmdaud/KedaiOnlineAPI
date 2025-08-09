using MediatR;

namespace KedaiOnline.Application.Items.Commands.DeleteItems;

public class DeleteItemsCommand(int kedaiId) : IRequest
{
    public int KedaiId { get; } = kedaiId;
}

