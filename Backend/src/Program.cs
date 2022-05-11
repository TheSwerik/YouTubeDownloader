using Backend.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(provider => new DownloadService(builder.Configuration["ffmpegPath"]));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(
    policyBuilder =>
    {
        // policyBuilder.WithOrigins("https://localhost:7258")
        // policyBuilder.WithOrigins("http://localhost:8080")
        policyBuilder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .WithExposedHeaders("*");
    }
);

app.UseAuthorization();

app.MapControllers();

app.Run();