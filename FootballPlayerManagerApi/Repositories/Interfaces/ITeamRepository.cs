using FootballPlayerManagerApi.Services.PlayersService;

namespace FootballPlayerManagerApi.Repositories.Interfaces;

public interface ITeamRepository
{
    IEnumerable<Player> GetTeamsPlayers(string id);
    bool AddPlayerToTeam(string id);
    bool DeletePlayerFromTeam(object id);
}