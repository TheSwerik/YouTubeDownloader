using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Backend.Service.Exception;

namespace Backend.Service;

public class DownloadService
{
    private const long MaxFileSizeInBytes = 100 * 1024 * 1024; // 100 MB limit
    private static readonly SemaphoreSlim ConcurrencySemaphore = new(3);

    private readonly string[] _arguments =
    {
        "--update",
#if DEBUG
        "--ffmpeg-location", "\"C:/Program Files/ffmpeg/bin\"",
#endif
        "--parse-metadata", "\"%(uploader|)s:%(meta_artist)s\"",
        "--embed-metadata",
        "--embed-thumbnail",
        "--extract-audio",
        "--format", "bestaudio[ext=m4a]",
        "--audio-format", "m4a",
        "--audio-quality", "0",
        "-o", $"\"%(title)s {Guid.NewGuid()}.%(ext)s\""
    };

    private readonly ILogger<DownloadService> _logger;

    public DownloadService(ILogger<DownloadService> logger)
    {
        _logger = logger;
    }

    public async Task<string?> DownloadYouTubeAudio(string url, string guid, int index = 0)
    {
        await ConcurrencySemaphore.WaitAsync();

        try
        {
            var dir = Directory.CreateDirectory(guid);

            // Request Validation: Check file size before downloading
            var fileSize = await GetVideoFileSizeAsync(url);
            if (fileSize is > MaxFileSizeInBytes)
            {
                _logger.LogWarning("Download rejected: File size {Size} exceeds limit of {Limit}", fileSize, MaxFileSizeInBytes);
                throw new YouTubeVideoDownloadException("File size exceeds the 500MB limit.");
            }

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
        finally
        {
            ConcurrencySemaphore.Release();
        }
    }

    private async Task<long?> GetVideoFileSizeAsync(string url)
    {
        try
        {
            var processStartInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "yt-dlp",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                StandardOutputEncoding = new UTF8Encoding(),
                StandardErrorEncoding = new UTF8Encoding()
            };

            processStartInfo.ArgumentList.Add("--print");
            processStartInfo.ArgumentList.Add("filesize");
            processStartInfo.ArgumentList.Add("--format");
            processStartInfo.ArgumentList.Add("bestaudio[ext=m4a]");
            processStartInfo.ArgumentList.Add(url);

            using var process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();
            var output = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (string.IsNullOrWhiteSpace(output)) return null;

            var matches = Regex.Match(output, @"(\d+)");
            if (matches.Success && long.TryParse(matches.Groups[1].Value, out var size)) return size;

            return null;
        }
        catch (System.Exception ex)
        {
            _logger.LogError("Error checking file size: {Msg}", ex.Message);
            return null; // Fail-open for UX if metadata check fails
        }
    }
}