using Frontend.Service;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages;

public partial class Index
{
    private string Url { get; set; } = "";
    private bool IsLoading { get; set; }
    [Inject] private DownloadService DownloadService { get; set; } = null!;

    private async void Submit()
    {
        IsLoading = true;
        await DownloadService.DownloadSong(Url);
        IsLoading = false;
        StateHasChanged();
    }
}