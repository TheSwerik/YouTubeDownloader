﻿using Blazored.Toast.Services;
using Frontend.Service.Util;
using Microsoft.JSInterop;

namespace Frontend.Service;

public class DownloadService : Service
{
    public DownloadService(HttpClient http,
                           IToastService toastService,
                           IJSRuntime js,
                           ExceptionLocalizationService exceptionLocalizationService)
        : base(http, toastService, exceptionLocalizationService)
    {
        Js = js;
    }

    private IJSRuntime Js { get; }

    public async Task<bool> DownloadSong(string url, int index = 0)
    {
        var response = await GetAsync($"download/song?url={url}&index={index}");

        if (!response.IsSuccessStatusCode)
        {
            var exceptionBody = await response.Content.GetExceptionBody();
            ToastService.ShowError(ExceptionLocalizationService[exceptionBody.Type, exceptionBody.Body!]);
            return false;
        }

        var bytes = await response.Content.ReadAsByteArrayAsync();
        var fileStream = new MemoryStream(bytes);
        using var streamRef = new DotNetStreamReference(fileStream);
        await Js.InvokeVoidAsync(
            "downloadFileFromStream",
            response.Content.Headers.ContentDisposition?.FileNameStar,
            streamRef
        );
        ToastService.ShowSuccess("Erfolgreich heruntergeladen.");
        return true;
    }
}