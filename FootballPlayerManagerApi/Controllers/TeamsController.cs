using FootballPlayerManagerApi.Services.Implementations;
using FootballPlayerManagerApi.Services.Interfaces;
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
        throw new NotImplementedException();
    }

    [HttpPut, Route("player")]
    public IActionResult AddPlayerToTeam(string id)
    {
        
        throw new NotImplementedException();
    }

    [HttpDelete, Route("player")]
    public IActionResult DeletePlayerFromTeam(string id)
    {
        
        throw new NotImplementedException();
    }
}