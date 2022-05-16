namespace Shared.Exception;

public record YouTubeDownloaderExceptionBody(ExceptionType Type, string Message, object? Body = null)
{
    public ExceptionType Type { get; } = Type;
    public string Message { get; } = Message;
    public object? Body { get; } = Body;
}