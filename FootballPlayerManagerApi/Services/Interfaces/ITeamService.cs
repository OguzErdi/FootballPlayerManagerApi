using FootballPlayerManagerApi.Contracts;
using FootballPlayerManagerApi.Entities;

namespace FootballPlayerManagerApi.Services.Interfaces;

public interface ITeamService
{
    Task<ServiceResponse<List<string>>> GetTeamsPlayersAsync(string id);
    Task<ServiceResponse<bool>> AddPlayerToTeamAsync(string id, string playerId);
    Task<ServiceResponse<bool>> DeletePlayerFromTeamAsync(string id, string playerId);
}