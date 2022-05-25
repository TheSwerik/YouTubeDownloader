using System.Diagnostics;
using Backend.Service.Exception;
using Backend.Util;
using MediaToolkit;
using MediaToolkit.Model;
using VideoLibrary;
using VideoLibrary.Exceptions;

namespace Backend.Service;

public class DownloadService
{
    private readonly ILogger<DownloadService> _logger;

    private readonly YouTube _youtube = YouTube.Default;

    // private readonly string _ffmpegPath;
    private string _ffmpegPath;

    // public DownloadService(string ffmpegPath)
    public DownloadService(ILogger<DownloadService> logger)
    {
        _logger = logger;
        // _ffmpegPath = ffmpegPath;
    }

    public string DownloadYouTubeAudio(string url)
    {
        if (url.IsVideoId()) url = $"youtu.be/{url}";
        if (url.IsInvalidYouTubeUrl()) throw new InvalidUrlException(url);

        var process = new Process();

        var processStartInfo = new ProcessStartInfo
                               {
                                   WindowStyle = ProcessWindowStyle.Hidden,
                                   FileName = "yt-dlp",
                                   Arguments = $"--extract-audio --audio-format mp3 {url}",
                                   RedirectStandardOutput = true,
                                   RedirectStandardError = true,
                                   UseShellExecute = false
                               };

        process.StartInfo = processStartInfo;
        process.Start();

        var error = process.StandardError.ReadToEnd();
        var output = process.StandardOutput.ReadToEnd();

        _logger.LogError("{Error}", error);
        _logger.LogInformation("{Output}", output);

        const string searchText = "[ExtractAudio] Destination:";
        var result = output.Split("\n").FirstOrDefault(l => l.StartsWith(searchText));
        if (result is null) throw new YouTubeVideoNotFoundException("idk"); //TODO
        result = result[searchText.Length..].Trim();
        var newResult = result[..^"[123456789ab].mp3".Length].Trim() + ".mp3";
        File.Move(result, newResult);

        //[ExtractAudio] Destination: =LOVE（イコールラブ）_ 11th Single c_w『知らんけど』【MV full】 [IplzPaWoFXo].mp3

        _logger.LogInformation("{Output}", result);
        _logger.LogInformation("{Output}", newResult);
        return newResult;
    }

    // public string DownloadYouTubeAudio(string url)
    // {
    //     _ffmpegPath = "C:\\Program Files\\FFMPEG\\bin\\ffmpeg.exe";
    //     var stopWatch = new Stopwatch();
    //     stopWatch.Start();
    //     var video = GetVideo(url);
    //     stopWatch.Stop();
    //     Console.WriteLine("Get Video: "+stopWatch.ElapsedMilliseconds);
    //
    //     stopWatch.Restart();
    //     var bytes = video.GetBytes();
    //     stopWatch.Stop();
    //     Console.WriteLine("Get Bytes: "+stopWatch.ElapsedMilliseconds);
    //
    //     stopWatch.Restart();
    //     var mp3 = DownloadMp3(video.FullName, bytes);
    //     stopWatch.Stop();
    //     Console.WriteLine("Get MP3: "+stopWatch.ElapsedMilliseconds);
    //
    //     stopWatch.Restart();
    //     var result = mp3.AddMp3TagsToFile(video.Title, video.Info.Author);
    //     stopWatch.Stop();
    //     Console.WriteLine("Add Tags: "+stopWatch.ElapsedMilliseconds);
    //     return result;
    // }

    #region Helper Methods

    private YouTubeVideo GetVideo(string url)
    {
        if (url.IsVideoId()) url = $"youtu.be/{url}";
        if (url.IsInvalidYouTubeUrl()) throw new InvalidUrlException(url);
        try
        {
            return _youtube.GetVideo(url);
        }
        catch (UnavailableStreamException e)
        {
            throw new YouTubeVideoNotFoundException(url);
        }
    }

    private string DownloadMp3(string filename, byte[] bytes)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        var videoPath = Path.Combine(filename);
        var audioPath = Path.ChangeExtension(videoPath, ".mp3");
        stopWatch.Stop();
        Console.WriteLine("Path: " + stopWatch.ElapsedMilliseconds);

        stopWatch.Restart();
        File.WriteAllBytes(videoPath, bytes);
        stopWatch.Stop();
        Console.WriteLine("WriteAllBytes: " + stopWatch.ElapsedMilliseconds);

        stopWatch.Restart();
        var inputFile = new MediaFile { Filename = videoPath };
        var outputFile = new MediaFile { Filename = audioPath };
        stopWatch.Stop();
        Console.WriteLine("MediaFile: " + stopWatch.ElapsedMilliseconds);

        stopWatch.Restart();
        using (var engine = new Engine(_ffmpegPath))
        {
            engine.GetMetadata(inputFile);
            engine.Convert(inputFile, outputFile);
        }

        stopWatch.Stop();
        Console.WriteLine("Convert: " + stopWatch.ElapsedMilliseconds);

        stopWatch.Restart();
        File.Delete(videoPath);
        stopWatch.Stop();
        Console.WriteLine("Delete: " + stopWatch.ElapsedMilliseconds);

        return audioPath;
    }

    #endregion
}