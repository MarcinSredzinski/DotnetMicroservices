using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers;

[Route("api/c/[controller]")]
[ApiController]
public class PlatformsController : Controller
{
    private readonly ILogger<PlatformsController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public PlatformsController(ILogger<PlatformsController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    public IActionResult TestInboundConnection()
    {
        using var client = _httpClientFactory.CreateClient("PlatformsHttpClient");

        var response = await client.PostAsJsonAsync($"platforms/CreatePlatform", platform);

        var result = response.Content;
        var responseBody = await result.ReadFromJsonAsync<PlatformReadDto>();

        return CreatedAtRoute(nameof(GetById), new { responseBody?.Id }, responseBody);
    }
}