using Backend.Service.Exception.Util;

namespace Backend.Service.Exception;

public class YouTubeVideoNotFoundException : NotFoundException
{
    public YouTubeVideoNotFoundException(string url) : base($"Cannot find YouTube-Video with URL: {url}") { }
}