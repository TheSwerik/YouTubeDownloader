using Frontend.Service;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages;

public partial class Index
{
    [Parameter] [SupplyParameterFromQuery(Name = "url")] public string? Url { get; set; }

    // private string Url { get; set; } = "";
    private bool IsLoading { get; set; }
    [Inject] private DownloadService DownloadService { get; set; } = null!;
    protected override Task OnInitializedAsync() { return Submit(); }
    protected override Task OnParametersSetAsync() { return Submit(); }

    private async Task Submit()
    {
        if (Url is null) return;
        IsLoading = true;
        await DownloadService.DownloadSong(Url);
        IsLoading = false;
        Url = null;
        StateHasChanged();
    }
}