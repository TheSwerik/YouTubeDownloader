using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Service.Exception;
using Microsoft.Extensions.Logging;

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

    public DownloadService(ILogger<DownloadService> logger)
    {
        _logger = logger;
    }

    public async Task<string?> DownloadYouTubeAudio(string url, string guid, int index = 0)
    {
        var dir = Directory.CreateDirectory(guid);
        var processStartInfo = new ProcessStartInfo
        {
            WindowStyle = ProcessWindowStyle.Hidden,
            FileName = "yt-dlp",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            WorkingDirectory = guid,
            StandardOutputEncoding = new UTF8Encoding(),
            StandardErrorEncoding = new UTF8Encoding()
        };

        foreach (var argument in _arguments) processStartInfo.ArgumentList.Add(argument);
        processStartInfo.ArgumentList.Add(index != 0 ? $"-I {index}" : "--no-playlist");
        // The URL is added as a final, distinct argument. ArgumentList handles the escaping automatically.
        processStartInfo.ArgumentList.Add(url);

        using var process = new Process();
        process.StartInfo = processStartInfo;
        process.Start();
        await process.WaitForExitAsync();

        var error = await process.StandardError.ReadToEndAsync();
        var output = await process.StandardOutput.ReadToEndAsync();

        if (error.Length > 0) _logger.LogError("{Error}", error);

        if (index != 0)
        {
            const string searchTextNoDownloads = "Downloading 0 items of";
            var foundLine = output.Split("\n").FirstOrDefault(l => l.Contains(searchTextNoDownloads));
            if (foundLine is not null) return null;
        }

        var files = dir.GetFiles();
        if (files.Length == 0) throw new YouTubeVideoDownloadException(url);
        return files[0].FullName;
    }
}