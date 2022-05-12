using Backend.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var ffmpegPath = builder.Configuration["FFMPEG_PATH"];
if (ffmpegPath == null) throw new Exception("FFMPEG_PATH is missing"); //TODO exceptions
builder.Services.AddSingleton(_ => new DownloadService(ffmpegPath));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(
    policyBuilder =>
    {
        // policyBuilder.WithOrigins("http://localhost:4200")
        policyBuilder.WithOrigins("http://a:1")
                     // policyBuilder.WithOrigins("http://localhost:4200","http://frontend:4200")
                     // policyBuilder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .WithExposedHeaders("*");
    }
);

app.UseAuthorization();

app.MapControllers();

app.Run();