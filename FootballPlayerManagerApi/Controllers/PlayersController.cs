using FootballPlayerManagerApi.Services.Implementations;
using FootballPlayerManagerApi.Services.Interfaces;
using FootballPlayerManagerApi.Services.PlayersService;
using Microsoft.AspNetCore.Mvc;

namespace FootballPlayerManagerApi.Controllers;

[ApiController]
[Route("[controller]/{id}")]
public class PlayersController : ControllerBase
{
    private readonly IPlayerService _playerService;
    private readonly ILogger<PlayersController> _logger;

    public PlayersController(ILogger<PlayersController> logger, IPlayerService playerService)
    {
        _logger = logger;
        _playerService = playerService;
    }

    [HttpGet, Route("")]
    public IActionResult GetPlayer(string id)
    {
        var player = _playerService.GetPlayer(id);
        return Ok(true);
    }

    [HttpPut, Route("")]
    public IActionResult UpdatePlayer(string id)
    {
        var player = _playerService.UpdatePlayer(id);
        return Ok(true);
    }
}