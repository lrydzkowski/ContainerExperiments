using Microsoft.AspNetCore.Mvc;
using AppInfoDto1 = ContainerExperiments.WebApi1.Client.Models.AppInfoDto;
using AppInfoDto3 = ContainerExperiments.WebApi3.Client.Models.AppInfoDto;
using IAppInfoService1 = ContainerExperiments.WebApi1.Client.Services.IAppInfoService;
using IAppInfoService3 = ContainerExperiments.WebApi3.Client.Services.IAppInfoService;

namespace ContainerExperiments.WebApi2.Controllers;

[ApiController]
[Route("")]
public class AppController : ControllerBase
{
    private readonly IAppInfoService1 _appInfoService1;
    private readonly IAppInfoService3 _appInfoService3;

    public AppController(IAppInfoService1 appInfoService1, IAppInfoService3 appInfoService3)
    {
        _appInfoService1 = appInfoService1;
        _appInfoService3 = appInfoService3;
    }

    [HttpGet]
    public IActionResult GetInfo()
    {
        return Ok(
            new
            {
                App = "WebApi2",
                Version = "1.0.0.1"
            }
        );
    }

    [HttpGet("api1")]
    public async Task<IActionResult> GetWebApi1Info(CancellationToken cancellationToken)
    {
        AppInfoDto1? appInfo = await _appInfoService1.GetAppInfoAsync(cancellationToken);

        return Ok(appInfo);
    }

    [HttpGet("api3")]
    public async Task<IActionResult> GetWebApi3Info(CancellationToken cancellationToken)
    {
        AppInfoDto3? appInfo = await _appInfoService3.GetAppInfoAsync(cancellationToken);

        return Ok(appInfo);
    }
}
