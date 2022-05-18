using System.Text;
using Blazored.Toast;
using Frontend;
using Frontend.Service;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

Console.OutputEncoding = Encoding.UTF8;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
       .AddLocalization()
       .AddScoped(_ => new HttpClient
                       {
                           BaseAddress = new Uri(
                               builder.HostEnvironment.IsEnvironment("Local")
                                   ? "http://localhost:8080"
                                   : builder.HostEnvironment.BaseAddress + "/api/"
                           )
                       })
       .AddScoped<ExceptionLocalizationService>()
       .AddScoped<UILocalizationService>()
       .AddScoped<DownloadService>()
       .AddBlazoredToast();

builder.Build().RunAsync();