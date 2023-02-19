using FootballPlayerManagerApi.Services.PlayersService;

namespace FootballPlayerManagerApi.Repositories.Interfaces;

public interface IPlayerRepository
{
    public Task<Player?> GetPlayerAsync(string id);

    public bool UpdatePlayerAsync(string id);
}