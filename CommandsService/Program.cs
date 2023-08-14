using CommandsService.Options;
using Polly;
using Polly.Contrib.WaitAndRetry;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
//builder.Services.AddScoped<ApiKeyAuthFilter>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#region Named client
builder.Services.AddHttpClient("PlatformsHttpClient", client =>
{
    var platformServiceOptions = new PlatformServiceOptions();
    builder.Configuration.GetSection(PlatformServiceOptions.PlatformService).Bind(platformServiceOptions);

    client.BaseAddress = new Uri(platformServiceOptions.BaseUrl);
    client.DefaultRequestHeaders.Add("X-Api-Key", platformServiceOptions.ApiKey);
    client.Timeout = new TimeSpan(0, 0, platformServiceOptions.Timeout);
})
.ConfigurePrimaryHttpMessageHandler(() =>
new HttpClientHandler
{
    UseCookies = false //Disables cookie container being shared between pooled httpmessagehandler
})
.AddTransientHttpErrorPolicy(policyBuilder =>                               //Handles the most common http exceptions - network error, server error (5xx) and request timeouts (408) 
    policyBuilder.WaitAndRetryAsync(
        Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5)));  //Adds randomization of the delay between retries and number of retries equal to 5
#endregion

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.MapControllers();

app.Run();
