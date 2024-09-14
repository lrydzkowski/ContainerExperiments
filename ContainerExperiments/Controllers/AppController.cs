using Microsoft.AspNetCore.Mvc;

namespace ContainerExperiments.Controllers;

[ApiController]
[Route("api")]
public class AppController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { AppName = "ContainerExperiments" });
    }

    [HttpGet]
    [Route("test")]
    public IActionResult Test()
    {
        return Ok(new { AppName = "ContainerExperiments Test 3" });
    }
}
