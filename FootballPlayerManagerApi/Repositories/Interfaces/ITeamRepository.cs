using FootballPlayerManagerApi.Entities;

namespace FootballPlayerManagerApi.Repositories.Interfaces;

public interface ITeamRepository
{
    Task<IEnumerable<Player>> GetTeamsPlayersAsync(string id);
    bool AddPlayerToTeamAsync(string id);
    bool DeletePlayerFromTeamAsync(object id);
    Task<Team> GetTeamAsync(string id);
}