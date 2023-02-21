using AutoMapper;
using FootballPlayerManagerApi.Constants;
using FootballPlayerManagerApi.Contracts;
using FootballPlayerManagerApi.Entities;
using FootballPlayerManagerApi.Repositories.Interfaces;
using FootballPlayerManagerApi.Services.Interfaces;

namespace FootballPlayerManagerApi.Services.Implementations;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IMapper _mapper;

    public TeamService(ITeamRepository teamRepository, IPlayerRepository playerRepository, IMapper mapper)
    {
        _teamRepository = teamRepository;
        _playerRepository = playerRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<List<string>>> GetTeamsPlayersAsync(string id)
    {
        ServiceResponse<List<string>> serviceResponse = new();

        var team = await _teamRepository.GetTeamAsync(id);
        if (team is null)
        {
            serviceResponse.ErrorMessage = ErrorMessages.TeamNotFound;
            return serviceResponse;
        }

        var playerNames = await GetPlayerNames(team);
        if (!playerNames.Any())
        {
            serviceResponse.ErrorMessage = ErrorMessages.TeamNotHavePlayers;
            return serviceResponse;
        }

        serviceResponse.Data = playerNames;

        return serviceResponse;
    }

    private async Task<List<string>> GetPlayerNames(Team team)
    {
        var playerNames = new List<string>();
        foreach (var playerId in team.PlayerIds)
        {
            var player = await _playerRepository.GetPlayerAsync(playerId);
            //can return error if player not found, but this way we won't interrupt the call.
            if (player is not null) playerNames.Add(player.Name);
        }

        return playerNames;
    }

    public bool AddPlayerToTeamAsync(string id)
    {
        throw new NotImplementedException();
    }

    public bool DeletePlayerFromTeamAsync(string id)
    {
        throw new NotImplementedException();
    }
}