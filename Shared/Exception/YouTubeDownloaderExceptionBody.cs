namespace Shared.Exception;

public record YouTubeDownloaderExceptionBody(ExceptionType Type, string Body)
{
    public ExceptionType Type { get; } = Type;
    public string Body { get; } = Body;
}