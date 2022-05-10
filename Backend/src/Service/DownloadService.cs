using MediaToolkit;
using MediaToolkit.Model;
using VideoLibrary;

namespace Backend.Service;

public class DownloadService
{
    // public byte[] DownloadYouTubeAudio(string url)
    // {
    //     const string outputDir = @"D:\Projects\YouTubeDownloader\Backend\out\";
    //
    //     var youtube = YouTube.Default;
    //     var vid = youtube.GetVideo(url);
    //
    //     var videoPath = Path.Combine(outputDir, vid.FullName);
    //     var audioPath = Path.ChangeExtension(videoPath, ".mp3");
    //     File.WriteAllBytes(videoPath, vid.GetBytes());
    //
    //     var inputFile = new MediaFile { Filename = videoPath };
    //     var outputFile = new MediaFile { Filename = audioPath };
    //
    //     using var engine = new Engine(@"C:\Program Files\FFMPEG\bin\ffmpeg.exe");
    //     engine.GetMetadata(inputFile);
    //     engine.Convert(inputFile, outputFile);
    //
    //     File.Delete(videoPath);
    //
    //     return File.ReadAllBytes(audioPath);
    // }
    public string DownloadYouTubeAudio(string url)
    {
        const string outputDir = @"D:\Projects\YouTubeDownloader\Backend\out\";

        var youtube = YouTube.Default;
        var vid = youtube.GetVideo(url);

        var videoPath = Path.Combine(outputDir, vid.FullName);
        var audioPath = Path.ChangeExtension(videoPath, ".mp3");
        File.WriteAllBytes(videoPath, vid.GetBytes());

        var inputFile = new MediaFile { Filename = videoPath };
        var outputFile = new MediaFile { Filename = audioPath };

        using var engine = new Engine(@"C:\Program Files\FFMPEG\bin\ffmpeg.exe");
        engine.GetMetadata(inputFile);
        engine.Convert(inputFile, outputFile);

        File.Delete(videoPath);

        return audioPath;
    }
}