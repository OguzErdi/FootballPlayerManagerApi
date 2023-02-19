using FootballPlayerManagerApi.Constants;
using FootballPlayerManagerApi.Contracts;
using FootballPlayerManagerApi.Entities;
using FootballPlayerManagerApi.Repositories.Implementations;
using FootballPlayerManagerApi.Repositories.Interfaces;
using FootballPlayerManagerApi.Services.Interfaces;

namespace FootballPlayerManagerApi.Services.Implementations;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<ServiceResponse<Player>> GetPlayerAsync(string id)
    {
        ServiceResponse<Player> serviceResponse = new();

        var player = await _playerRepository.GetPlayerAsync(id);
        if (player is null)
        {
            serviceResponse.ErrorMessage = ErrorMessages.PlayerNotFound;
        }
        else
        {
            serviceResponse.Data = player;
        }


        return serviceResponse;
    }

    public bool UpdatePlayerAsync(string id)
    {
        return _playerRepository.UpdatePlayerAsync(id);
    }
}