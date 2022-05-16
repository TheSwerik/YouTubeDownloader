using System.Net.Http.Json;
using Blazored.Toast.Services;
using Microsoft.JSInterop;
using Shared.Exception;

namespace Frontend.Service;

public class DownloadService : Service
{
    public DownloadService(HttpClient http, IToastService toastService, IJSRuntime js) : base(http, toastService)
    {
        Js = js;
    }

    private IJSRuntime Js { get; }

    public async Task DownloadSong(string url)
    {
        var response = await GetAsync($"download/song?url={url}");

        if (!response.IsSuccessStatusCode)
        {
            var exceptionBody = await response.Content.ReadFromJsonAsync<YouTubeDownloaderExceptionBody>() ??
                                new YouTubeDownloaderExceptionBody(ExceptionType.DEFAULT, "test");
            ToastService.ShowError(exceptionBody.Body, exceptionBody.Type.ToString());
            return;
        }

        var bytes = await response.Content.ReadAsByteArrayAsync();
        var fileStream = new MemoryStream(bytes);
        using var streamRef = new DotNetStreamReference(fileStream);
        await Js.InvokeVoidAsync(
            "downloadFileFromStream",
            response.Content.Headers.ContentDisposition?.FileName,
            streamRef
        );
        ToastService.ShowSuccess("Erfolgreich heruntergeladen.");
    }
}