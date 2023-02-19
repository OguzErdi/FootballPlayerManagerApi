using FootballPlayerManagerApi.Repositories.Interfaces;
using FootballPlayerManagerApi.Services.Interfaces;
using FootballPlayerManagerApi.Services.PlayersService;

namespace FootballPlayerManagerApi.Services.Implementations;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamsService;

    public TeamService(ITeamRepository teamsService)
    {
        _teamsService = teamsService;
    }

    public IEnumerable<Player> GetTeamsPlayersAsync(string id)
    {
        return _teamsService.GetTeamsPlayersAsync(id);
    }

    public bool AddPlayerToTeamAsync(string id)
    {
        return _teamsService.AddPlayerToTeamAsync(id);
    }

    public bool DeletePlayerFromTeamAsync(string id)
    {
        return _teamsService.DeletePlayerFromTeamAsync(id);
    }
}