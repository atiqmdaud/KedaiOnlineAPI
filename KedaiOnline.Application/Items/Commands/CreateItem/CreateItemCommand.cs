using MediatR;

namespace KedaiOnline.Application.Items.Commands.CreateItem;

public class CreateItemCommand : IRequest
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }

    public int KedaiId { get; set; }
}
