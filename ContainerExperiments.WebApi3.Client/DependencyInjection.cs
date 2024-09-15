using ContainerExperiments.WebApi3.Client.Options;
using ContainerExperiments.WebApi3.Client.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContainerExperiments.WebApi3.Client;

public static class DependencyInjection
{
    public static void ConfigureWebApi3ClientServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.ConfigureOptions<WebApi3Options>(configuration, WebApi3Options.Position);
        services.AddScoped<IAppInfoService, AppInfoService>();
        services.AddWebApi3HttpClient();
    }

    private static void ConfigureOptions<TOptions>(
        this IServiceCollection services,
        IConfiguration configuration,
        string configurationPosition
    )
        where TOptions : class
    {
        services.AddOptions<TOptions>().Bind(configuration.GetSection(configurationPosition)).ValidateOnStart();
    }
}
