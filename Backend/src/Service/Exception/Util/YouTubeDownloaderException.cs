namespace Backend.Service.Exception.Util;

public abstract class YouTubeDownloaderException : System.Exception
{
    protected YouTubeDownloaderException(int statusCode, object? value = null)
    {
        (StatusCode, Value) = (statusCode, value);
    }

    public int StatusCode { get; }

    public object? Value { get; }
}