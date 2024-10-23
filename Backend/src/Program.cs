using System.Reflection;
using System.Text;
using Backend.Service;
using Backend.Service.Exception.Util;

Console.OutputEncoding = Encoding.UTF8;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => { options.Filters.Add<HttpResponseExceptionFilter>(); });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


var ffmpegPath = builder.Configuration["FFMPEG_PATH"];
if (ffmpegPath == null) throw new Exception("FFMPEG_PATH is missing"); //TODO exceptions

#region Services

builder.Services.AddSingleton<DownloadService>();

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(
    policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .WithExposedHeaders("*");
    }
);

app.UseAuthorization();

app.MapControllers();

app.Run();