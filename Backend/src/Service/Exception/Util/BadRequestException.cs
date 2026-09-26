using Shared.Exception;

namespace Backend.Service.Exception.Util;

public class BadRequestException : YouTubeDownloaderException
{
    protected BadRequestException(YouTubeDownloaderExceptionBody? body) : base(400, body)
    {
    }
}