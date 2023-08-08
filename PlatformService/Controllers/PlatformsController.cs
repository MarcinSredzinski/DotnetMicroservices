using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Authentication;
using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Models;

namespace PlatformService.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PlatformsController : Controller
{
    private readonly IPlatformRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<PlatformsController> _logger;

    public PlatformsController(IPlatformRepository repo, IMapper mapper, ILogger<PlatformsController> logger)
    {
        _repository = repo;
        _mapper = mapper;
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
    public ActionResult<PlatformReadDto> CreatePlatform([FromBody] PlatformCreateDto platform)
    {
        _logger.LogInformation("--> Creating Platform");
        var platformModel = _mapper.Map<Platform>(platform);
        _repository.Create(platformModel);
       
        var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

        if (platformReadDto is null)
        {
            return NotFound();
        }
        return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
    }
}