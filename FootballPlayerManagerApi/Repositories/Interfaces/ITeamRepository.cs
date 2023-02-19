using FootballPlayerManagerApi.Services.PlayersService;

namespace FootballPlayerManagerApi.Repositories.Interfaces;

public interface ITeamRepository
{
    IEnumerable<Player> GetTeamsPlayersAsync(string id);
    bool AddPlayerToTeamAsync(string id);
    bool DeletePlayerFromTeamAsync(object id);
}