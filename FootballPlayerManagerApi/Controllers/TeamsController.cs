using FootballPlayerManagerApi.Services.Implementations;
using FootballPlayerManagerApi.Services.Interfaces;
using FootballPlayerManagerApi.Services.PlayersService;
using Microsoft.AspNetCore.Mvc;

namespace FootballPlayerManagerApi.Controllers;

[ApiController]
[Route("[controller]/{id}")]
public class TeamsController : ControllerBase
{
    private readonly ITeamService _teamService;
    private readonly ILogger<TeamsController> _logger;

    public TeamsController(ILogger<TeamsController> logger, ITeamService teamService)
    {
        _logger = logger;
        _teamService = teamService;
    }

    [HttpGet, Route("players")]
    public IActionResult GetTeamsPlayers(string id)
    {
        var players = _teamService.GetTeamsPlayers(id);
        return Ok(true);
    }

    [HttpPut, Route("player")]
    public IActionResult AddPlayerToTeam(string id)
    {
        
        var result = _teamService.AddPlayerToTeam(id);
        return Ok(true);
    }

    [HttpDelete, Route("player")]
    public IActionResult DeletePlayerFromTeam(string id)
    {
        
        var result = _teamService.DeletePlayerFromTeam(id);
        return Ok(true);
    }
}