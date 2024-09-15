﻿using ContainerExperiments.WebApi1.Models;
using ContainerExperiments.WebApi1.Services;
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
    private readonly IFileService _fileService;

    public AppController(
        IAppInfoService2 appInfoService2,
        IAppInfoService3 appInfoService3,
        IFileService fileService
    )
    {
        _appInfoService2 = appInfoService2;
        _appInfoService3 = appInfoService3;
        _fileService = fileService;
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

    [HttpPost("file")]
    public async Task<IActionResult> SaveFile(SaveFileRequest request, CancellationToken cancellationToken)
    {
        await _fileService.SaveFileAsync(request.Content, cancellationToken);

        return Ok();
    }
}
