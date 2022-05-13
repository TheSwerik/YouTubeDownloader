using Backend.Service.Exception.Util;

namespace Backend.Service.Exception;

public class InvalidUrlException : BadRequestException
{
    public InvalidUrlException(string url) : base($"{url} is not a valid YouTube-URL") { }
}