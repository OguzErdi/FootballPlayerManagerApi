using System.Net;
using FootballPlayerManagerApi.Constants;
using FootballPlayerManagerApi.Contracts;
using FootballPlayerManagerApi.Entities;
using FootballPlayerManagerApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FootballPlayerManagerApi.Controllers;

[ApiController]
[Route("[controller]/{id}")]
public class TeamsController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamsController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpGet, Route("players")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Get team's player list", typeof(ServiceResponse<List<Player>>))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Return not found if team not found or player list is empty",
        typeof(ServiceResponse<List<Player>>))]
    public async Task<IActionResult> GetTeamsPlayers(string id)
    {
        var response = await _teamService.GetTeamsPlayersAsync(id);
        return response.HasError switch
        {
            true when response.ErrorMessage.Equals(ErrorMessages.TeamNotFound) ||
                      response.ErrorMessage.Equals(ErrorMessages.TeamNotHavePlayers) => NotFound(response),
            _ => Ok(response)
        };
    }

    [HttpPut, Route("player/{playerId}")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Add player to team", typeof(ServiceResponse<bool>))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Return not found if team or player not found",
        typeof(ServiceResponse<bool>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError,
        "Return internal server error if an unexpected exception throw", typeof(ServiceResponse<bool>))]
    public async Task<IActionResult> AddPlayerToTeam(string id, string playerId)
    {
        var response = await _teamService.AddPlayerToTeamAsync(id, playerId);
        return response.HasError switch
        {
            true when response.ErrorMessage.Equals(ErrorMessages.PlayerNotFound) ||
                      response.ErrorMessage.Equals(ErrorMessages.TeamNotFound) => NotFound(response),
            true when response.ErrorMessage.Equals(ErrorMessages.ProcessFailed) => StatusCode(500, response),
            _ => Ok(response)
        };
    }

    [HttpDelete, Route("player/{playerId}")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Delete player from team", typeof(ServiceResponse<bool>))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Return not found if team not found", typeof(ServiceResponse<bool>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError,
        "Return internal server error if an unexpected exception throw", typeof(ServiceResponse<bool>))]
    public async Task<IActionResult> DeletePlayerFromTeam(string id, string playerId)
    {
        var response = await _teamService.DeletePlayerFromTeamAsync(id, playerId);
        return response.HasError switch
        {
            true when response.ErrorMessage.Equals(ErrorMessages.TeamNotFound) => NotFound(response),
            true when response.ErrorMessage.Equals(ErrorMessages.ProcessFailed) => StatusCode(500, response),
            _ => Ok(response)
        };
    }
}