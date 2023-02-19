using FootballPlayerManagerApi.Contracts;
using FootballPlayerManagerApi.Entities;
using FootballPlayerManagerApi.Repositories.Interfaces;
using FootballPlayerManagerApi.Services.Interfaces;

namespace FootballPlayerManagerApi.Services.Implementations;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamsService;

    public TeamService(ITeamRepository teamsService)
    {
        _teamsService = teamsService;
    }

    public async Task<ServiceResponse<IEnumerable<Player>>> GetTeamsPlayersAsync(string id)
    {
        throw new NotImplementedException();
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