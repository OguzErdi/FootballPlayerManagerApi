using FootballPlayerManagerApi.Services.PlayersService;

namespace FootballPlayerManagerApi.Services.Interfaces;

public interface ITeamService
{
    IEnumerable<Player> GetTeamsPlayers(string id);
    bool AddPlayerToTeam(string id);
    bool DeletePlayerFromTeam(string id);
}