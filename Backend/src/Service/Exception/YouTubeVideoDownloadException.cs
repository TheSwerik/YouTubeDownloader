using Backend.Service.Exception.Util;
using Shared.Exception;

namespace Backend.Service.Exception;

public class YouTubeVideoDownloadException : BadRequestException
{
    public YouTubeVideoDownloadException(string url) : base(
        new YouTubeDownloaderExceptionBody(
            ExceptionType.CANT_DOWNLOAD,
            $"Cannot download YouTube-Video with URL: {url}",
            url
        )
    )
    {
    }
}