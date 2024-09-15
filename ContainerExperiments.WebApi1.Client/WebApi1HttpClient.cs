using ContainerExperiments.WebApi1.Client.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ContainerExperiments.WebApi1.Client;

internal static class WebApi1HttpClient
{
    public static IServiceCollection AddWebApi1HttpClient(this IServiceCollection services)
    {
        services.AddHttpClient(
            nameof(WebApi1HttpClient),
            (serviceProvider, client) =>
            {
                IOptions<WebApi1Options> options =
                    serviceProvider.GetRequiredService<IOptions<WebApi1Options>>();
                client.BaseAddress = new Uri(options.Value.BaseUrl);
            }
        );

        return services;
    }
}
