using ContainerExperiments.WebApi3.Client.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ContainerExperiments.WebApi3.Client;

internal static class WebApi3HttpClient
{
    public static IServiceCollection AddWebApi3HttpClient(this IServiceCollection services)
    {
        services.AddHttpClient(
            nameof(WebApi3HttpClient),
            (serviceProvider, client) =>
            {
                IOptions<WebApi3Options> options =
                    serviceProvider.GetRequiredService<IOptions<WebApi3Options>>();
                client.BaseAddress = new Uri(options.Value.BaseUrl);
            }
        );

        return services;
    }
}
