using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "PlatformService", Version = "v1" });
//});
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseInMemoryDatabase("InMem"));
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
//builder.Services.AddScoped<ApiKeyAuthFilter>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
PrepDb.PrepPopulation(app);
// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
