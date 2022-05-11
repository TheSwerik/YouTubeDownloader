using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Frontend.Service;

public class DownloadService : Service
{
    public DownloadService(HttpClient http, IJSRuntime js) : base(http) { Js = js; }

    [Inject] private IJSRuntime Js { get; set; }

    public async void DownloadSong(string url)
    {
        var response = await Http.GetAsync($"/api/download/song?url={url}");
        response.EnsureSuccessStatusCode();
        var bytes = await response.Content.ReadAsByteArrayAsync();
        var fileStream = new MemoryStream(bytes);
        using var streamRef = new DotNetStreamReference(fileStream);
        await Js.InvokeVoidAsync(
            "downloadFileFromStream",
            response.Content.Headers.ContentDisposition?.FileName,
            streamRef
        );
    }
}