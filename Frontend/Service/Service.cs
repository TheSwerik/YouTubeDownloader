using Blazored.Toast.Services;

namespace Frontend.Service;

public abstract class Service
{
    protected Service(HttpClient http, IToastService toastService)
    {
        Http = http;
        ToastService = toastService;
    }

    protected IToastService ToastService { get; }
    protected HttpClient Http { get; }

    protected async Task<HttpResponseMessage> GetAsync(string url)
    {
        try
        {
            return await Http.GetAsync(url);
        }
        catch (HttpRequestException)
        {
            ToastService.ShowError("Verbindung zum Server konnte nicht aufgebaut werden.");
            throw;
        }
    }
}