using Backend.Service;
using Backend.Service.Exception;
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
    /// <exception cref="YouTubeVideoDownloadException">400 if the video cannot be downloaded.</exception>
    /// <returns>The downloaded MP3-File with content-type "audio/mpeg".</returns>
    /// <response code="200">Returns the downloaded MP3-File with content-type "audio/mpeg".</response>
    /// <response code="400">If the URL is not a valid YouTube-URL.</response>
    /// <response code="400">If the video cannot be downloaded.</response>
    [HttpGet("song")]
    [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces("audio/mpeg", "application/json")]
    public IActionResult Get(string url)
    {
        var guid = Guid.NewGuid().ToString();
        Response.OnCompleted(() => Task.Run(() => Directory.Delete(guid, true)));
        var filePath = _downloadService.DownloadYouTubeAudio(url, guid);
        var fileStream = new FileStream(
            filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.None,
            4096,
            FileOptions.DeleteOnClose
        );
        return File(fileStream, "audio/mpeg", Path.GetFileName(filePath));
    }
}