using ContainerExperiments.WebApi1.Client.Options;
using ContainerExperiments.WebApi1.Client.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContainerExperiments.WebApi1.Client;

public static class DependencyInjection
{
    public static void ConfigureWebApi1ClientServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.ConfigureOptions<WebApi1Options>(configuration, WebApi1Options.Position);
        services.AddScoped<IAppInfoService, AppInfoService>();
        services.AddWebApi1HttpClient();
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
