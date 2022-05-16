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

    /// <summary>Downloads a YouTube-Video as a MP3 File.</summary>
    /// <param name="url">The URL for the YouTube-Video to be downloaded.</param>
    /// <exception cref="Backend.Service.Exception.InvalidUrlException">400 if the URL is not a valid YouTube-URL.</exception>
    /// <exception cref="Backend.Service.Exception.YouTubeVideoNotFoundException">404 if the URL does not lead to a video.</exception>
    /// <returns>The downloaded MP3-File with content-type "audio/mpeg".</returns>
    /// <response code="200">Returns the downloaded MP3-File with content-type "audio/mpeg".</response>
    /// <response code="400">If the URL is not a valid YouTube-URL.</response>
    /// <response code="404">If the URL does not lead to a video.</response>
    [HttpGet("song")]
    [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces("audio/mpeg", "application/json")]
    public IActionResult Get(string url)
    {
        var path = _downloadService.DownloadYouTubeAudio(url);
        var fileStream = new FileStream(
            path,
            FileMode.Open,
            FileAccess.Read,
            FileShare.None,
            4096,
            FileOptions.DeleteOnClose
        );
        return File(fileStream, "audio/mpeg", Path.GetFileName(path));
    }
}