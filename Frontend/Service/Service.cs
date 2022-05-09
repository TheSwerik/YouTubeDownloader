using Microsoft.AspNetCore.Components;

namespace Frontend.Service;

public abstract class Service
{
    protected readonly HttpClient Http;
    protected readonly NavigationManager NavigationManager;

    protected Service(HttpClient http, NavigationManager navigationManager)
    {
        Http = http;
        NavigationManager = navigationManager;
    }
}