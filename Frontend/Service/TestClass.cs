using Microsoft.AspNetCore.Components;

namespace Frontend.Service;

public class TestClass : Service
{
    public TestClass(HttpClient http, NavigationManager navigationManager) : base(http, navigationManager) { }

    public async Task<string> test()
    {
        Console.WriteLine("hheheheheh");
        Console.WriteLine(Http.BaseAddress);
        var result = await Http.GetAsync("/WeatherForecast/test");
        Console.WriteLine(result);
        Console.WriteLine(result.Content);
        return await result.Content.ReadAsStringAsync();
    }
}