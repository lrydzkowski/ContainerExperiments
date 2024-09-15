using ContainerExperiments.WebApi2.Client.Options;
using ContainerExperiments.WebApi2.Client.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContainerExperiments.WebApi2.Client;

public static class DependencyInjection
{
    public static void ConfigureWebApi2ClientServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.ConfigureOptions<WebApi2Options>(configuration, WebApi2Options.Position);
        services.AddScoped<IAppInfoService, AppInfoService>();
        services.AddWebApi2HttpClient();
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
