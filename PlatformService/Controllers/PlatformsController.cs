using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;

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
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        _logger.LogInformation("--> Getting Platforms");

        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(_repository.GetAll()));
    }

    [Route("{id}")]
    [HttpGet]
    public ActionResult<PlatformReadDto> GetPlatformById(int id)
    {
        _logger.LogInformation("--> Getting Platforms");
        var platform = _mapper.Map<PlatformReadDto>(_repository.Get(id));
        if (platform is null)
        {
            return NotFound();
        }
        return Ok(platform);
    }

}