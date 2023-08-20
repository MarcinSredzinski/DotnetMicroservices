using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PlatformsController : Controller
{
    private readonly IPlatformRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICommandDataClient _commandDataClient;
    private readonly ILogger<PlatformsController> _logger;

    public PlatformsController( ILogger<PlatformsController> logger, IPlatformRepository repo, IMapper mapper, ICommandDataClient commandDataClient)
    {
        _repository = repo;
        _mapper = mapper;
        _commandDataClient = commandDataClient;
        _logger = logger;
    }

    [HttpGet]
    //[ServiceFilter(typeof(ApiKeyAuthFilter))]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        _logger.LogInformation("--> Getting Platforms");

        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(_repository.GetAll()));
    }

    [Route("{id}", Name = "GetPlatformById")]
    [HttpGet]
    public ActionResult<PlatformReadDto> GetPlatformById(int id)
    {
        _logger.LogInformation("--> Getting Platform by id");
        var platform = _mapper.Map<PlatformReadDto>(_repository.Get(id));
        if (platform is null)
        {
            return NotFound();
        }
        return Ok(platform);
    }

    [HttpPost]
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform([FromBody] PlatformCreateDto platform)
    {
        _logger.LogInformation("--> Creating Platform");
        var platformModel = _mapper.Map<Platform>(platform);
        _repository.Create(platformModel);
       
        var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

        try
        {
            await _commandDataClient.SendPlatformToCommand(platformReadDto);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not send synchronously to command service because: {0}", e.Message);           
        }
        
        return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
    }
}