namespace Backend.Service.Exception.Util;

public class NotFoundException : YouTubeDownloaderException
{
    public NotFoundException(object? value = null) : base(404, value) { }
}