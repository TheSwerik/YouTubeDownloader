using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.Exception;

namespace Frontend.Util;

public static class ExtensionMethods
{
    public static async Task<YouTubeDownloaderExceptionBody> GetExceptionBody(this HttpContent content)
    {
        return await content.ReadFromJsonAsync<YouTubeDownloaderExceptionBody>() ??
               new YouTubeDownloaderExceptionBody(ExceptionType.Default, "Failure");
    }
}