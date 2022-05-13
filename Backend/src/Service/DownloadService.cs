using Backend.Service.Exception;
using Backend.Util;
using MediaToolkit;
using MediaToolkit.Model;
using VideoLibrary;
using VideoLibrary.Exceptions;

namespace Backend.Service;

public class DownloadService
{
    private readonly string _ffmpegPath;
    private readonly YouTube _youtube = YouTube.Default;

    public DownloadService(string ffmpegPath) { _ffmpegPath = ffmpegPath; }

    public string DownloadYouTubeAudio(string url)
    {
        var video = GetVideo(url);
        return DownloadMp3(video.FullName, video.GetBytes()).AddMp3TagsToFile(video.Title, video.Info.Author);
    }

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
        var videoPath = Path.Combine(filename);
        var audioPath = Path.ChangeExtension(videoPath, ".mp3");

        File.WriteAllBytes(videoPath, bytes);

        var inputFile = new MediaFile { Filename = videoPath };
        var outputFile = new MediaFile { Filename = audioPath };

        using (var engine = new Engine(_ffmpegPath))
        {
            engine.GetMetadata(inputFile);
            engine.Convert(inputFile, outputFile);
        }

        File.Delete(videoPath);

        return audioPath;
    }

    #endregion
}