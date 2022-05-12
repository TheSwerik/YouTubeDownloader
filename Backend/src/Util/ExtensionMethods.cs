using System.Text.RegularExpressions;
using File = TagLib.File;

namespace Backend.Util;

public static class ExtensionMethods
{
    public static bool IsVideoId(this string text) { return Regex.IsMatch(text, "^[A-Za-z0-9-_]{11}$"); }

    public static bool IsInvalidYouTubeUrl(this string url)
    {
        return !Regex.IsMatch(url, "^(https{0,1}://){0,1}(www.){0,1}(youtube.com|youtu.be).+$");
    }

    public static string AddMp3TagsToFile(this string filePath, string title, params string[] artists)
    {
        var tagFile = File.Create(filePath);
        tagFile.Tag.Title = title;
        tagFile.Tag.Performers = artists;
        tagFile.Save();
        return filePath;
    }
}