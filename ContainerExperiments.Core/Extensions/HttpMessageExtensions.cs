using System.Net;
using System.Text;
using System.Text.Json;
using ContainerExperiments.Core.Models;

namespace ContainerExperiments.Core.Extensions;

public static class HttpMessageExtensions
{
    public static async Task ThrowIfNotSuccessAsync(
        this HttpResponseMessage response,
        HttpStatusCode expectedHttpStatusCode = HttpStatusCode.OK
    )
    {
        await response.ThrowIfNotSuccessAsync([expectedHttpStatusCode]);
    }

    public static async Task ThrowIfNotSuccessAsync(
        this HttpResponseMessage response,
        IReadOnlyList<HttpStatusCode> expectedHttpStatusCodes
    )
    {
        if (expectedHttpStatusCodes.Contains(response.StatusCode))
        {
            return;
        }

        string errorMessage = await response.BuildUnrecognizedResponseErrorMessageAsync();
        throw new HttpRequestException(errorMessage);
    }

    public static async Task<T?> GetResponseAsync<T>(this HttpResponseMessage response)
    {
        string message = await response.Content.ReadAsStringAsync();
        T? payload = JsonSerializer.Deserialize<T>(
            message,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }
        );

        return payload;
    }

    private static async Task<string> BuildUnrecognizedResponseErrorMessageAsync(this HttpResponseMessage response)
    {
        HttpRequestMessage? request = response.RequestMessage;
        string requestPayload = request?.Content is null ? "" : await request.Content.ReadAsStringAsync();
        string responsePayload = await response.Content.ReadAsStringAsync();

        StringBuilder errorMessageBuilder = new();
        errorMessageBuilder.AppendLine($"Unrecognized response from {request?.Method} {request?.RequestUri}.");
        errorMessageBuilder.AppendLine($"Request payload: '{requestPayload}'.");
        errorMessageBuilder.AppendLine($"Response status: {response.StatusCode}.");
        errorMessageBuilder.AppendLine($"Response payload: '{responsePayload}'.");
        if (request?.Headers is not null)
        {
            errorMessageBuilder.AppendLine("Request headers:");
            foreach (KeyValuePair<string, IEnumerable<string>> header in request.Headers)
            {
                string headerName = header.Key;
                if (HttpHeaderNames.HeadersToFilterFromLogs.Contains(
                        headerName,
                        StringComparer.InvariantCultureIgnoreCase
                    ))
                {
                    continue;
                }

                errorMessageBuilder.AppendLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }
        }

        string errorMessage = errorMessageBuilder.ToString();

        return errorMessage;
    }
}
