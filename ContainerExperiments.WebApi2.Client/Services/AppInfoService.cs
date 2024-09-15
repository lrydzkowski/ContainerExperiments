using ContainerExperiments.Core.Extensions;
using ContainerExperiments.WebApi2.Client.Models;

namespace ContainerExperiments.WebApi2.Client.Services;

public interface IAppInfoService
{
    Task<AppInfoDto?> GetAppInfoAsync(CancellationToken cancellationToken);
}

internal class AppInfoService
    : IAppInfoService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AppInfoService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<AppInfoDto?> GetAppInfoAsync(CancellationToken cancellationToken)
    {
        HttpClient client = _httpClientFactory.CreateClient(nameof(WebApi2HttpClient));
        HttpRequestMessage requestMessage = new(HttpMethod.Get, "");
        HttpResponseMessage responseMessage = await client.SendAsync(requestMessage, cancellationToken);
        await responseMessage.ThrowIfNotSuccessAsync();

        AppInfoDto? appInfoDb = await responseMessage.GetResponseAsync<AppInfoDto>();

        return appInfoDb;
    }
}
