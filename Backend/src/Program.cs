using System.Text;
using Backend.Service;
using Backend.Service.Exception.Util;

Console.OutputEncoding = Encoding.UTF8;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
//TODO Broken:
// builder.Services.AddSwaggerGen(options =>
// {
//     var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//     options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
// });

builder.Services.AddControllers(options => { options.Filters.Add<HttpResponseExceptionFilter>(); });

var ffmpegPath = builder.Configuration["FFMPEG_PATH"];
if (ffmpegPath == null) throw new Exception("FFMPEG_PATH is missing"); //TODO exceptions
builder.Services.AddSingleton<DownloadService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //TODO Broken:
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseCors(policyBuilder =>
    {
        // In a production environment, replace AllowAnyOrigin() with specific allowed origins.
        if (app.Environment.IsDevelopment())
            policyBuilder.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader()
                         .WithExposedHeaders("*");
        else
            // Placeholder for production CORS configuration
            policyBuilder.WithOrigins("https://yourdomain.com")
                         .AllowAnyMethod()
                         .AllowAnyHeader();
    }
);

app.UseAuthorization();

app.MapControllers();

app.Run();