using KedaiOnline.Application.KedaiOnline;
using KedaiOnline.Application.KedaiOnline.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace KedaiOnline.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KedaiOnlineController(IKedaiOnlineService kedaiOnlineService):ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
        {
        var kedaiOnline = await kedaiOnlineService.GetAllKedaiOnline();
        return Ok(kedaiOnline);
    }

    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetById(int id)
    //{

    //}

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        var kedai = await kedaiOnlineService.GetKedaiOnlineById(id);
        return kedai is not null ? Ok(kedai) : NotFound();
    }

    [HttpPost]
    //public async Task<IActionResult> Create([FromBody] CreateKedaiOnlineCommand command)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }
    //    var kedai = await kedaiOnlineService.CreateKedaiOnline(command);
    //    return CreatedAtAction(nameof(GetById), new { id = kedai.Id }, kedai);
    //}

    public async Task<IActionResult> CreateKedai(CreateKedaiDto createKedaiDto )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        int id = await kedaiOnlineService.Create(createKedaiDto);
        return CreatedAtAction(nameof(GetById), new { id }, null);

    }

}
