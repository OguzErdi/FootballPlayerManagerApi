using FootballPlayerManagerApi.Contracts;
using FootballPlayerManagerApi.Entities;

namespace FootballPlayerManagerApi.Services.Interfaces;

public interface ITeamService
{
    Task<ServiceResponse<IEnumerable<Player>>> GetTeamsPlayersAsync(string id);
    bool AddPlayerToTeamAsync(string id);
    bool DeletePlayerFromTeamAsync(string id);
}