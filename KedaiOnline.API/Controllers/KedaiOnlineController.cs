using KedaiOnline.Application.KedaiOnline;
using KedaiOnline.Application.KedaiOnline.Commands.CreateKedai;
using KedaiOnline.Application.KedaiOnline.Commands.DeleteKedai;
using KedaiOnline.Application.KedaiOnline.Commands.UpdateKedai;
using KedaiOnline.Application.KedaiOnline.Dtos;
using KedaiOnline.Application.KedaiOnline.Queries.GetAllKedaiOnline;
using KedaiOnline.Application.KedaiOnline.Queries.GetKedaiById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KedaiOnline.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class KedaiOnlineController(IMediator mediator):ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
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
        return Ok(kedai);
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
        await mediator.Send(new DeleteKedaiCommand(id));
        return NoContent();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateKedai([FromRoute] int id, UpdateKedaiCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }

}
