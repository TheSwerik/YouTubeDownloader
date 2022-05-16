using Shared.Exception;

namespace Backend.Service.Exception.Util;

public class NotFoundException : YouTubeDownloaderException
{
    protected NotFoundException(YouTubeDownloaderExceptionBody? body) : base(404, body) { }
}