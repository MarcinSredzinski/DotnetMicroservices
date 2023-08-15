using CommandsService.Models;
using CommandsService.Options;
using System.Text;
using System.Text.Json;

namespace CommandsService.SyncDataServices.Http;

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _httpClient;
    ILogger<HttpCommandDataClient> _logger;

    public HttpCommandDataClient(ILogger<HttpCommandDataClient> logger, HttpClient httpClient, PlatformServiceOptions options)
    {
        _logger = logger;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(options.BaseUrl);
    }

    public async Task SendPlatformToCommand(PlatformReadDto platform)
    {
        var httpContent = new StringContent(JsonSerializer.Serialize(platform), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_httpClient.BaseAddress, httpContent);

        if (response.IsSuccessStatusCode)
        { 
            _logger.LogInformation("Successfully posted platform to platform service.");
            return;
        }

        _logger.LogWarning("There has been a problem when posting platform to platform service.");
    }
}
