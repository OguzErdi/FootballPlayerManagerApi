using FootballPlayerManagerApi.Entities;

namespace FootballPlayerManagerApi.Repositories.Interfaces;

public interface ITeamRepository
{
    Task UpdatePlayerIdsOnTeam(string id, List<string> playerIds);
    Task<Team> GetTeamAsync(string id);
    Task<bool> IsTeamExist(string id);
    Task<List<string>?> GetPlayerIdsFromTeam(string id);
}