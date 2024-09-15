using Microsoft.AspNetCore.Mvc;
using AppInfoDto1 = ContainerExperiments.WebApi1.Client.Models.AppInfoDto;
using AppInfoDto2 = ContainerExperiments.WebApi2.Client.Models.AppInfoDto;
using IAppInfoService1 = ContainerExperiments.WebApi1.Client.Services.IAppInfoService;
using IAppInfoService2 = ContainerExperiments.WebApi2.Client.Services.IAppInfoService;

namespace ContainerExperiments.WebApi3.Controllers;

[ApiController]
[Route("")]
public class AppController : ControllerBase
{
    private readonly IAppInfoService1 _appInfoService1;
    private readonly IAppInfoService2 _appInfoService2;

    public AppController(IAppInfoService1 appInfoService1, IAppInfoService2 appInfoService2)
    {
        _appInfoService1 = appInfoService1;
        _appInfoService2 = appInfoService2;
    }

    [HttpGet]
    public IActionResult GetInfo()
    {
        return Ok(
            new
            {
                App = "WebApi3",
                Version = "1.0.0.0"
            }
        );
    }

    [HttpGet("api1")]
    public async Task<IActionResult> GetWebApi1Info(CancellationToken cancellationToken)
    {
        AppInfoDto1? appInfo = await _appInfoService1.GetAppInfoAsync(cancellationToken);

        return Ok(appInfo);
    }

    [HttpGet("api2")]
    public async Task<IActionResult> GetWebApi2Info(CancellationToken cancellationToken)
    {
        AppInfoDto2? appInfo = await _appInfoService2.GetAppInfoAsync(cancellationToken);

        return Ok(appInfo);
    }
}
