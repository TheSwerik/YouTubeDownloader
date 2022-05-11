using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Frontend.Service;

public class DownloadService : Service
{
    public DownloadService(HttpClient http, IJSRuntime js) : base(http) { Js = js; }

    [Inject] private IJSRuntime Js { get; set; }

    public async void DownloadSong(string url)
    {
        // url = "pwTzHbIXSlI";
        Console.WriteLine(Http.BaseAddress);
        Console.WriteLine(Http.BaseAddress + $"api/download/song?url={url}");
        Console.WriteLine(Http.DefaultRequestHeaders);
        var response = await Http.GetAsync($"/api/download/song?url={url}");
        // await Http.GetAsync($"api/download/song?url={url}");
        // await Http.GetAsync($"http://localhost:4200/api/download/song?url={url}");
        // await Http.GetAsync($"http://backend/api/download/song?url={url}");
        // await Http.GetAsync($"http://backend:8080/api/download/song?url={url}");
        Console.WriteLine(response.StatusCode);
        Console.WriteLine(response.Headers);
        Console.WriteLine(response.Content.Headers);
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