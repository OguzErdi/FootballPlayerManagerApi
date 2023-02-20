using AutoMapper;
using FootballPlayerManagerApi.Constants;
using FootballPlayerManagerApi.Contracts;
using FootballPlayerManagerApi.Contracts.Request;
using FootballPlayerManagerApi.Entities;
using FootballPlayerManagerApi.Repositories.Interfaces;
using FootballPlayerManagerApi.Services.Interfaces;

namespace FootballPlayerManagerApi.Services.Implementations;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IMapper _mapper;

    public PlayerService(IPlayerRepository playerRepository, IMapper mapper)
    {
        _playerRepository = playerRepository;
        _mapper = mapper;
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

    public async Task<ServiceResponse<bool>> UpdatePlayerAsync(string id, PlayerUpdateRequest request)
    {
        ServiceResponse<bool> serviceResponse = new();

        var isExist = await _playerRepository.IsPlayerExist(id);
        if (!isExist)
        {
            serviceResponse.ErrorMessage = ErrorMessages.PlayerNotFound;
            return serviceResponse;
        }

        var player = _mapper.Map<Player>(request);
        await _playerRepository.UpdatePlayerAsync(id, player);
        serviceResponse.Data = true;
        
        return serviceResponse;
    }
}