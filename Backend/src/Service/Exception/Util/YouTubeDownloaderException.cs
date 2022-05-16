using Shared.Exception;

namespace Backend.Service.Exception.Util;

public abstract class YouTubeDownloaderException : System.Exception
{
    protected YouTubeDownloaderException(int statusCode, YouTubeDownloaderExceptionBody? body)
    {
        (StatusCode, Body) = (statusCode, body);
    }

    public int StatusCode { get; }

    public YouTubeDownloaderExceptionBody? Body { get; }
}