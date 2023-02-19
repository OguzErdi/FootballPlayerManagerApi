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

    public async Task<Player> GetPlayer(string id)
    {
        return await _playerRepository.GetPlayer(id);
    }

    public bool UpdatePlayer(string id)
    {
        return _playerRepository.UpdatePlayer(id);
    }
}