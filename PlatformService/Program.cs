using Microsoft.EntityFrameworkCore;
using PlatformService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "PlatformService", Version = "v1" });
});
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseInMemoryDatabase("InMem"));
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

PrepDb.PrepPopulation(app);
// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();