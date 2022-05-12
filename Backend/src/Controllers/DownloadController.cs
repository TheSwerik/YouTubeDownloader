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

    [HttpGet("song")]
    public IActionResult Get(string url)
    {
        _logger.LogInformation("{%s}", HttpContext.Request.Headers.Origin);
        var path = _downloadService.DownloadYouTubeAudio(url);
        var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None, 4096,
                                        FileOptions.DeleteOnClose);
        return File(fileStream, "audio/mpeg", Path.GetFileName(path));
    }
}