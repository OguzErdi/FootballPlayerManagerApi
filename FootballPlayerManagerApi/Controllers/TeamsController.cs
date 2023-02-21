using FootballPlayerManagerApi.Constants;
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
    public async Task<IActionResult> GetTeamsPlayers(string id)
    {
        var response = await _teamService.GetTeamsPlayersAsync(id);

        if (response.HasError && response.ErrorMessage.Equals(ErrorMessages.TeamNotFound) &&
            response.ErrorMessage.Equals(ErrorMessages.TeamNotHavePlayers))
        {
            return NotFound(response);
        }

        return Ok(response);
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