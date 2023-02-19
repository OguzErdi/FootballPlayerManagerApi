using FootballPlayerManagerApi.Services.PlayersService;

namespace FootballPlayerManagerApi.Services.Interfaces;

public interface ITeamService
{
    IEnumerable<Player> GetTeamsPlayersAsync(string id);
    bool AddPlayerToTeamAsync(string id);
    bool DeletePlayerFromTeamAsync(string id);
}