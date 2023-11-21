using Frontend.Service;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages;

public partial class Index
{
    [Parameter] [SupplyParameterFromQuery(Name = "url")] public string? Url { get; set; }

    // private string Url { get; set; } = "";
    private bool IsLoading { get; set; }
    private bool DownloadEntirePlaylist { get; set; }
    [Inject] private DownloadService DownloadService { get; set; } = null!;
    protected override Task OnInitializedAsync() { return Submit(); }
    protected override Task OnParametersSetAsync() { return Submit(); }

    private async Task Submit()
    {
        if (Url is null) return;
        IsLoading = true;

        if (!DownloadEntirePlaylist) await DownloadService.DownloadSong(Url); //download only the one song
        for (var i = 1; DownloadEntirePlaylist; i++)
            DownloadEntirePlaylist = await DownloadService.DownloadSong(Url, i);

        IsLoading = false;
        Url = null;
        StateHasChanged();
    }
}