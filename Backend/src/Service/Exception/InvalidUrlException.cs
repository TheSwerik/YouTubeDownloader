using Backend.Service.Exception.Util;
using Shared.Exception;

namespace Backend.Service.Exception;

public class InvalidUrlException : BadRequestException
{
    public InvalidUrlException(string url) : base(
        new YouTubeDownloaderExceptionBody(ExceptionType.InvalidUrl, $"{url} is not a valid YouTube-URL", url)
    )
    {
    }
}