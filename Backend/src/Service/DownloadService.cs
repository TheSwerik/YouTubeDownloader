using System.Diagnostics;
using Backend.Service.Exception;

namespace Backend.Service;

public class DownloadService
{
    private readonly string[] _arguments =
    {
        "--update",
        #if DEBUG
        "--ffmpeg-location \"C:/Program Files/ffmpeg/bin\"",
        #endif
        "--parse-metadata \"%(uploader|)s:%(meta_artist)s\"",
        "--embed-metadata",
        "--embed-thumbnail",
        "--extract-audio",
        "--format bestaudio[ext=m4a]",
        "--audio-format m4a",
        "--audio-quality 0",
        $"-o \"%(title)s {Guid.NewGuid()}.%(ext)s\""
    };

    private readonly ILogger<DownloadService> _logger;

    public DownloadService(ILogger<DownloadService> logger) { _logger = logger; }

    public string DownloadYouTubeAudio(string url, string guid)
    {
        Directory.CreateDirectory(guid);
        var processStartInfo = new ProcessStartInfo
                               {
                                   WindowStyle = ProcessWindowStyle.Hidden,
                                   FileName = "yt-dlp",
                                   Arguments = string.Join(' ', _arguments) + $" {url}",
                                   RedirectStandardOutput = true,
                                   RedirectStandardError = true,
                                   UseShellExecute = false,
                                   WorkingDirectory = guid
                               };

        var process = new Process { StartInfo = processStartInfo };
        process.Start();
        process.WaitForExit();

        var error = process.StandardError.ReadToEnd();
        var output = process.StandardOutput.ReadToEnd();

        if (error.Length > 0) _logger.LogError("{Error}", error);

        const string searchText = "[download] Destination:";
        var result = output.Split("\n").FirstOrDefault(l => l.StartsWith(searchText));
        if (result is null) throw new YouTubeVideoDownloadException(url);
        return $"{guid}/{result[searchText.Length..].Trim()}";
    }
}