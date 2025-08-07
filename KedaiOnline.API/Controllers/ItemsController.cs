using KedaiOnline.Application.Items.Commands.CreateItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KedaiOnline.API.Controllers;

[Route("api/kedaionline/{kedaiId}/items")]
[ApiController]
public class ItemsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateItem([FromRoute]int kedaiId, CreateItemCommand command)
    { 
        command.KedaiId = kedaiId;
        await mediator.Send(command);
        return Created();

    }
}
