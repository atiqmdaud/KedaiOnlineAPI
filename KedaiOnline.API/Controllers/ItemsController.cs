using KedaiOnline.Application.Items.Commands.CreateItem;
using KedaiOnline.Application.Items.Dtos;
using KedaiOnline.Application.Items.Queries.GetItem;
using KedaiOnline.Application.Items.Queries.GetItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KedaiOnline.API.Controllers;

[Route("api/kedaionline/{kedaiId}/items")]
[ApiController]
public class ItemsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateItem([FromRoute] int kedaiId, CreateItemCommand command)
    {
        command.KedaiId = kedaiId;
        await mediator.Send(command);
        return Created();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems([FromRoute] int kedaiId)
    {
        var items = await mediator.Send(new GetItemsQuery(kedaiId));
        return Ok(items);
    }

    [HttpGet("{itemId}")]
    public async Task<ActionResult<ItemDto>> GetItem([FromRoute] int kedaiId, [FromRoute]int itemId)
    {
        var item = await mediator.Send(new GetItemQuery(kedaiId, itemId));
        return Ok(item);
    }
}
