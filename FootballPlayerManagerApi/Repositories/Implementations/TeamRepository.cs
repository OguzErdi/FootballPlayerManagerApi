using FootballPlayerManagerApi.Repositories.Interfaces;
using FootballPlayerManagerApi.Services.PlayersService;

namespace FootballPlayerManagerApi.Repositories.Implementations;

public class TeamRepository : ITeamRepository
{
    public IEnumerable<Player> GetTeamsPlayersAsync(string id)
    {
        throw new NotImplementedException();
    }

    public bool AddPlayerToTeamAsync(string id)
    {
        throw new NotImplementedException();
    }

    public bool DeletePlayerFromTeamAsync(object id)
    {
        throw new NotImplementedException();
    }
}