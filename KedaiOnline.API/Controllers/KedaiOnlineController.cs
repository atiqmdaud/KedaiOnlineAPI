using KedaiOnline.Application.KedaiOnline;
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

}
