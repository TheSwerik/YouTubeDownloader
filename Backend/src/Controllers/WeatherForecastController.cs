using MediaToolkit;
using MediaToolkit.Model;
using Microsoft.AspNetCore.Mvc;
using VideoLibrary;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy",
        "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger) { _logger = logger; }

    [HttpGet("test")]
    public IEnumerable<WeatherForecast> Get()
    {
        DownloadYouTubeAudio("https://www.youtube.com/watch?v=pwTzHbIXSlI");

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                                                      {
                                                          Date = DateTime.Now.AddDays(index),
                                                          TemperatureC = Random.Shared.Next(-20, 55),
                                                          Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                                                      })
                         .ToArray();
    }

    [HttpGet("DownloadYouTubeAudio")]
    public void DownloadYouTubeAudio(string url)
    {
        const string outputDir = @"D:\Projects\YouTubeDownloader\Backend\out\";

        var youtube = YouTube.Default;
        var vid = youtube.GetVideo(url);

        var videoPath = Path.Combine(outputDir, vid.FullName);
        var audioPath = Path.ChangeExtension(videoPath, ".mp3");
        System.IO.File.WriteAllBytes(videoPath, vid.GetBytes());

        var inputFile = new MediaFile { Filename = videoPath };
        var outputFile = new MediaFile { Filename = audioPath };

        using var engine = new Engine(@"C:\Program Files\FFMPEG\bin\ffmpeg.exe");
        engine.GetMetadata(inputFile);
        engine.Convert(inputFile, outputFile);

        System.IO.File.Delete(videoPath);

        Console.WriteLine($"Saved {audioPath}");
    }
}