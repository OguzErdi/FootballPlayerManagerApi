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

    public IEnumerable<Player> GetTeamsPlayers(string id)
    {
        return _teamsService.GetTeamsPlayers(id);
    }

    public bool AddPlayerToTeam(string id)
    {
        return _teamsService.AddPlayerToTeam(id);
    }

    public bool DeletePlayerFromTeam(string id)
    {
        return _teamsService.DeletePlayerFromTeam(id);
    }
}