using Backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("download")]
public class DownloadController : ControllerBase
{
    private readonly DownloadService _downloadService;
    private readonly ILogger<DownloadController> _logger;

    public DownloadController(ILogger<DownloadController> logger, DownloadService downloadService)
    {
        _logger = logger;
        _downloadService = downloadService;
    }

    // [HttpGet("song")] public byte[] Get(string url) { return _downloadService.DownloadYouTubeAudio(url); }

    [HttpGet("song")]
    public IActionResult Get(string url)
    {
        var path = _downloadService.DownloadYouTubeAudio(url);
        return File(System.IO.File.OpenRead(path), "audio/mpeg", Path.GetFileName(path));
    }
}