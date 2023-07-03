using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;

namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : Controller
{
    private readonly IPlatformRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public PlatformsController(IPlatformRepository repo, IMapper mapper, ILogger logger)
    {
        _repository = repo;
        _mapper = mapper;
        _logger = logger;
    }
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        _logger.LogInformation("--> Getting Platforms");

        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(_repository.GetAll()));
    }
}