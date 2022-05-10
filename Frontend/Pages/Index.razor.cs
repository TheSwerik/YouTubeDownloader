﻿using Frontend.Service;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages;

public partial class Index
{
    private string Url { get; set; }
    [Inject] private DownloadService Test { get; set; }

    public void Submit() { Test.DownloadSong(Url); }
}