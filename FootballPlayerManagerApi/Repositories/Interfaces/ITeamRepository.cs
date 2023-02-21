using FootballPlayerManagerApi.Entities;

namespace FootballPlayerManagerApi.Repositories.Interfaces;

public interface ITeamRepository
{
    Task AddPlayerToTeamAsync(string id, string playerId);
    bool DeletePlayerFromTeamAsync(object id);
    Task<Team> GetTeamAsync(string id);
    Task<bool> IsTeamExist(string id);
}