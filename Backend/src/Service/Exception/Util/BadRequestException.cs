namespace Backend.Service.Exception.Util;

public class BadRequestException : YouTubeDownloaderException
{
    public BadRequestException(object? value = null) : base(400, value) { }
}