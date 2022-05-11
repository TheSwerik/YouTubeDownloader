using Frontend;
using Frontend.Service;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services
       .AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:4200") })
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost") });
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://backend:8080") });
       .AddScoped<DownloadService>();


await builder.Build().RunAsync();