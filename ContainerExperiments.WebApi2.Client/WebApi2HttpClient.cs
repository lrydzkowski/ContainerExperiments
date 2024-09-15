using ContainerExperiments.WebApi2.Client.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ContainerExperiments.WebApi2.Client;

internal static class WebApi2HttpClient
{
    public static IServiceCollection AddWebApi2HttpClient(this IServiceCollection services)
    {
        services.AddHttpClient(
            nameof(WebApi2HttpClient),
            (serviceProvider, client) =>
            {
                IOptions<WebApi2Options> options =
                    serviceProvider.GetRequiredService<IOptions<WebApi2Options>>();
                client.BaseAddress = new Uri(options.Value.BaseUrl);
            }
        );

        return services;
    }
}
