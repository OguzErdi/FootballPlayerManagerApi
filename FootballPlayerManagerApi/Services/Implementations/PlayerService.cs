using FootballPlayerManagerApi.Repositories.Implementations;
using FootballPlayerManagerApi.Repositories.Interfaces;
using FootballPlayerManagerApi.Services.Interfaces;
using FootballPlayerManagerApi.Services.PlayersService;

namespace FootballPlayerManagerApi.Services.Implementations;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<Player> GetPlayerAsync(string id)
    {
        return await _playerRepository.GetPlayerAsync(id);
    }

    public bool UpdatePlayerAsync(string id)
    {
        return _playerRepository.UpdatePlayerAsync(id);
    }
}