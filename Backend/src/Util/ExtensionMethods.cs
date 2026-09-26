using System.Text.RegularExpressions;

namespace Backend.Util;

public static class ExtensionMethods
{
    extension(string text)
    {
        public bool IsVideoId()
        {
            return Regex.IsMatch(text.Trim(), "^[A-Za-z0-9-_]{11}$");
        }

        public bool IsInvalidYouTubeUrl()
        {
            return !Regex.IsMatch(text.Trim(), "^(https{0,1}://){0,1}(www.){0,1}(youtube.com|youtu.be)/[a-zA-Z0-9&?=_-]+$");
        }
    }
}