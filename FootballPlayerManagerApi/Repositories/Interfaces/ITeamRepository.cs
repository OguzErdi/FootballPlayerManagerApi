using FootballPlayerManagerApi.Entities;

namespace FootballPlayerManagerApi.Repositories.Interfaces;

public interface ITeamRepository
{
    Task AddPlayerToTeamAsync(string id, string playerId);
    Task DeletePlayerFromTeamAsync(string id, string playerId);
    Task<Team> GetTeamAsync(string id);
    Task<bool> IsTeamExist(string id);
    Task<List<string>?> GetPlayerIdsFromTeam(string id);
}