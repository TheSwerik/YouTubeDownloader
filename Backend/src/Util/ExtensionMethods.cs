using System.Text.RegularExpressions;

namespace Backend.Util;

public static class ExtensionMethods
{
    public static bool IsVideoId(this string text) { return Regex.IsMatch(text.Trim(), "^[A-Za-z0-9-_]{11}$"); }

    public static bool IsInvalidYouTubeUrl(this string url)
    {
        return !Regex.IsMatch(url.Trim(), "^(https{0,1}://){0,1}(www.){0,1}(youtube.com|youtu.be)/.+$");
    }
}