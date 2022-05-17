using Frontend.Service;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages;

public partial class Index
{
    private string Url { get; set; } = "";
    [Inject] private DownloadService DownloadService { get; set; } = null!;

    private async void Submit() { await DownloadService.DownloadSong(Url); }
}