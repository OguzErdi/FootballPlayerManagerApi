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
    private readonly ILogger<TeamService> _logger;

    public TeamService(ITeamRepository teamRepository, IPlayerRepository playerRepository, IMapper mapper,
        ILogger<TeamService> logger)
    {
        _teamRepository = teamRepository;
        _playerRepository = playerRepository;
        _mapper = mapper;
        _logger = logger;
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

    public async Task<ServiceResponse<bool>> AddPlayerToTeamAsync(string id, string playerId)
    {
        ServiceResponse<bool> serviceResponse = new();

        var isTeamExist = await _teamRepository.IsTeamExist(id);
        if (!isTeamExist)
        {
            serviceResponse.ErrorMessage = ErrorMessages.TeamNotFound;
            return serviceResponse;
        }

        var isPlayerExist = await _playerRepository.IsPlayerExist(playerId);
        if (!isPlayerExist)
        {
            serviceResponse.ErrorMessage = ErrorMessages.PlayerNotFound;
            return serviceResponse;
        }

        try
        {
            await _teamRepository.AddPlayerToTeamAsync(id, playerId);
        }
        catch (Exception exception)
        {
            _logger.LogError("Exception: {Exception}", exception);
            serviceResponse.ErrorMessage = ErrorMessages.ProcessFailed;
            return serviceResponse;
        }


        serviceResponse.Data = true;
        return serviceResponse;
    }

    public Task<ServiceResponse<bool>> DeletePlayerFromTeamAsync(string id)
    {
        throw new NotImplementedException();
    }
}