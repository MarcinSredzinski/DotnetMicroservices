using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers;

[Route("api/c/[controller]")]
[ApiController]
public class PlatformsController : Controller
{
    [HttpPost]
    public IActionResult TestInboundConnection()
    {
        Console.WriteLine("--> Inpbound POST #Command Service");
        return Ok("Inbound test of Platforms Controller in Commands Service");
    }
}