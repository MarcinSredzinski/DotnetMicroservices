using PlatformService.DTOs;
using PlatformService.Options;
using System.Text;
using System.Text.Json;

namespace PlatformService.SyncDataServices.Http;

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _httpClient;
    ILogger<HttpCommandDataClient> _logger;

    public HttpCommandDataClient(ILogger<HttpCommandDataClient> logger, HttpClient httpClient, PlatformServiceOptions options)
    {
        _logger = logger;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(options.BaseUrl);
        _logger.LogInformation("Creating {0} with base address {1}", nameof(HttpCommandDataClient), _httpClient.BaseAddress);
    }

    public async Task SendPlatformToCommand(PlatformReadDto platform)
    {
        _logger.LogDebug("Sending platform to command.");
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
