using Backend.Service.Exception.Util;
using Shared.Exception;

namespace Backend.Service.Exception;

public class YouTubeVideoNotFoundException : NotFoundException
{
    public YouTubeVideoNotFoundException(string url) : base(
        new YouTubeDownloaderExceptionBody(ExceptionType.VIDEO_NOT_FOUND, $"Cannot find YouTube-Video with URL: {url}",
                                           url)
    )
    {
    }
}