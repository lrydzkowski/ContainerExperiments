using Microsoft.AspNetCore.Mvc;
using AppInfoDto2 = ContainerExperiments.WebApi2.Client.Models.AppInfoDto;
using AppInfoDto3 = ContainerExperiments.WebApi3.Client.Models.AppInfoDto;
using IAppInfoService2 = ContainerExperiments.WebApi2.Client.Services.IAppInfoService;
using IAppInfoService3 = ContainerExperiments.WebApi3.Client.Services.IAppInfoService;

namespace ContainerExperiments.WebApi1.Controllers;

[ApiController]
[Route("")]
public class AppController : ControllerBase
{
    private readonly IAppInfoService2 _appInfoService2;
    private readonly IAppInfoService3 _appInfoService3;

    public AppController(IAppInfoService2 appInfoService2, IAppInfoService3 appInfoService3)
    {
        _appInfoService2 = appInfoService2;
        _appInfoService3 = appInfoService3;
    }

    [HttpGet]
    public IActionResult GetInfo()
    {
        return Ok(
            new
            {
                App = "WebApi1",
                Version = "1.0.0.1"
            }
        );
    }

    [HttpGet("api2")]
    public async Task<IActionResult> GetWebApi2Info(CancellationToken cancellationToken)
    {
        AppInfoDto2? appInfo = await _appInfoService2.GetAppInfoAsync(cancellationToken);

        return Ok(appInfo);
    }

    [HttpGet("api3")]
    public async Task<IActionResult> GetWebApi3Info(CancellationToken cancellationToken)
    {
        AppInfoDto3? appInfo = await _appInfoService3.GetAppInfoAsync(cancellationToken);

        return Ok(appInfo);
    }
}
