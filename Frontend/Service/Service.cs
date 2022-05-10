namespace Frontend.Service;

public abstract class Service
{
    protected readonly HttpClient Http;

    protected Service(HttpClient http) { Http = http; }
}