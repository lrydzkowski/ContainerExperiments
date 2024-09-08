using Microsoft.AspNetCore.Mvc;

namespace ContainerExperiments.Controllers;

[ApiController]
[Route("api")]
public class AppController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(
            new
            {
                AppName = "ContainerExperiments"
            }
        );
    }
}
