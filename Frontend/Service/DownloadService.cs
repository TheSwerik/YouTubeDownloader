using Blazored.Toast.Services;
using Microsoft.JSInterop;

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

        Console.WriteLine(response.StatusCode);
        if (!response.IsSuccessStatusCode)
        {
            ToastService.ShowError(await response.Content.ReadAsStringAsync());
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