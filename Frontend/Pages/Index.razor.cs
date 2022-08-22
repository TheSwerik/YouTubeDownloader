using Frontend.Service;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages;

public partial class Index
{
    [Parameter] [SupplyParameterFromQuery(Name = "url")] public string? Url { get; set; }

    // private string Url { get; set; } = "";
    private bool IsLoading { get; set; }
    [Inject] private DownloadService DownloadService { get; set; } = null!;

    private async void Submit()
    {
        Console.WriteLine(Url);
        IsLoading = true;
        await DownloadService.DownloadSong(Url);
        IsLoading = false;
        StateHasChanged();
    }
}