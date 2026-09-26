using Blazored.Toast.Services;

namespace Frontend.Service;

public abstract class Service
{
    protected Service(HttpClient http,
                      IToastService toastService,
                      ExceptionLocalizationService exceptionLocalizationService)
    {
        Http = http;
        ToastService = toastService;
        ExceptionLocalizationService = exceptionLocalizationService;
    }

    protected IToastService ToastService { get; }
    protected HttpClient Http { get; }
    protected ExceptionLocalizationService ExceptionLocalizationService { get; }

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