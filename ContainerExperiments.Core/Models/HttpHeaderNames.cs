using Microsoft.Net.Http.Headers;

namespace ContainerExperiments.Core.Models;

internal static class HttpHeaderNames
{
    public static readonly IReadOnlyList<string> HeadersToFilterFromLogs =
    [
        HeaderNames.Authorization
    ];
}
