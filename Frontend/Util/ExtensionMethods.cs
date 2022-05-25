using System.Net.Http.Json;
using Shared.Exception;

namespace Frontend.Service.Util;

public static class ExtensionMethods
{
    public static async Task<YouTubeDownloaderExceptionBody> GetExceptionBody(this HttpContent content)
    {
        return await content.ReadFromJsonAsync<YouTubeDownloaderExceptionBody>() ??
               new YouTubeDownloaderExceptionBody(ExceptionType.Default, "Failure");
    }
}