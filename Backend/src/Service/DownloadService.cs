using System.Diagnostics;
using Backend.Service.Exception;
using Backend.Util;

namespace Backend.Service;

public class DownloadService
{
    private readonly ILogger<DownloadService> _logger;

    public DownloadService(ILogger<DownloadService> logger) { _logger = logger; }

    public string DownloadYouTubeAudio(string url)
    {
        if (url.IsVideoId()) url = $"youtu.be/{url}";
        if (url.IsInvalidYouTubeUrl()) throw new InvalidUrlException(url);


        var processStartInfo = new ProcessStartInfo
                               {
                                   WindowStyle = ProcessWindowStyle.Hidden,
                                   FileName = "yt-dlp",
                                   Arguments =
                                       $"--parse-metadata \"%(uploader|)s:%(meta_artist)s\" --add-metadata --extract-audio --audio-quality 0 --audio-format mp3 {url}",
                                   RedirectStandardOutput = true,
                                   RedirectStandardError = true,
                                   UseShellExecute = false
                               };

        var process = new Process { StartInfo = processStartInfo };
        process.Start();

        var error = process.StandardError.ReadToEnd();
        var output = process.StandardOutput.ReadToEnd();

        if (error.Length > 0) _logger.LogError("{Error}", error);

        const string searchText = "[ExtractAudio] Destination:";
        var result = output.Split("\n").FirstOrDefault(l => l.StartsWith(searchText));
        if (result is null) throw new YouTubeVideoDownloadException(url);
        result = result[searchText.Length..].Trim();
        var newResult = result[..^"[123456789ab].mp3".Length].Trim() + ".mp3";
        File.Move(result, newResult);

        return newResult;
    }
}