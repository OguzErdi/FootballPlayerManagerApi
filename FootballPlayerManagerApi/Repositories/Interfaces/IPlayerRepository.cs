using FootballPlayerManagerApi.Entities;

namespace FootballPlayerManagerApi.Repositories.Interfaces;

public interface IPlayerRepository
{
    public Task<Player?> GetPlayerAsync(string id);

    public bool UpdatePlayerAsync(string id);
}