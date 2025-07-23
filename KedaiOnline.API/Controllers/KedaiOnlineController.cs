using KedaiOnline.Application.KedaiOnline;
using KedaiOnline.Application.KedaiOnline.Commands.CreateKedai;
using KedaiOnline.Application.KedaiOnline.Commands.DeleteKedai;
using KedaiOnline.Application.KedaiOnline.Commands.UpdateKedai;
using KedaiOnline.Application.KedaiOnline.Dtos;
using KedaiOnline.Application.KedaiOnline.Queries.GetAllKedaiOnline;
using KedaiOnline.Application.KedaiOnline.Queries.GetKedaiById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KedaiOnline.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KedaiOnlineController(IMediator mediator):ControllerBase
{
    [HttpGet]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<KedaiDto>))]
    public async Task<ActionResult<IEnumerable<KedaiDto>>> GetAll()
        {
        var kedaiOnline = await mediator.Send(new GetAllKedaiOnlineQuery());
        return Ok(kedaiOnline);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<KedaiDto?>> GetById([FromRoute]int id)
    {
        var kedai = await mediator.Send(new GetKedaiByIdQuery(id));
        return kedai is not null ? Ok(kedai) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateKedai(CreateKedaiCommand command )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);

    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteKedai([FromRoute] int id)
    {
        var isDeleted = await mediator.Send(new DeleteKedaiCommand(id));
        return isDeleted ? NoContent() : NotFound();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateKedai([FromRoute] int id, UpdateKedaiCommand command)
    {
        command.Id = id;
        var isUpdated = await mediator.Send(command);
        return isUpdated ? NoContent() : NotFound();
    }

}
