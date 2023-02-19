using FootballPlayerManagerApi.Services.PlayersService;

namespace FootballPlayerManagerApi.Services.Interfaces;

public interface IPlayerService
{
    public Task<Player> GetPlayerAsync(string id);

    public bool UpdatePlayerAsync(string id);
}