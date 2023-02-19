using FootballPlayerManagerApi.Entities;
using FootballPlayerManagerApi.Repositories.Interfaces;

namespace FootballPlayerManagerApi.Repositories.Implementations;

public class TeamRepository : ITeamRepository
{
    public Task<IEnumerable<Player>> GetTeamsPlayersAsync(string id)
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