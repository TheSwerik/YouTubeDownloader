using Frontend.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Shared.Exception;
using ExceptionResource = Frontend.Resources.Exception;

namespace Frontend.Pages;

public partial class Index
{
    private string Url { get; set; } = "";
    [Inject] private DownloadService Test { get; set; } = null!;

    // [Inject] private ExceptionLocalizationService ExceptionLocalization { get; set; } = null!;
    [Inject] private IStringLocalizer<ExceptionResource> localizer { get; set; } = null!;

    private async void Submit()
    {
        Console.WriteLine(ExceptionType.DEFAULT.ToString());
        Console.WriteLine(localizer[ExceptionType.DEFAULT.ToString()]);
        await Test.DownloadSong(Url);
    }
}