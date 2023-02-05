using FootballPlayerManagerApi.Repositories.Interfaces;
using FootballPlayerManagerApi.Services.PlayersService;

namespace FootballPlayerManagerApi.Repositories.Implementations;

public class TeamRepository : ITeamRepository
{
    public IEnumerable<Player> GetTeamsPlayers(string id)
    {
        throw new NotImplementedException();
    }

    public bool AddPlayerToTeam(string id)
    {
        throw new NotImplementedException();
    }

    public bool DeletePlayerFromTeam(object id)
    {
        throw new NotImplementedException();
    }
}