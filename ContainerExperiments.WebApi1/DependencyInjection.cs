using ContainerExperiments.WebApi1.Options;
using ContainerExperiments.WebApi1.Services;

namespace ContainerExperiments.WebApi1;

public static class DependencyInjection
{
    public static void ConfigureWebApi1Services(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.ConfigureOptions<VolumeMountOptions>(configuration, VolumeMountOptions.Position);
        services.ConfigureOptions<TestOptions>(configuration, TestOptions.Position);
        services.AddScoped<IFileService, FileService>();
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
