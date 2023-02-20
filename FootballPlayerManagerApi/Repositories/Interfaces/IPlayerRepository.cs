using FootballPlayerManagerApi.Entities;

namespace FootballPlayerManagerApi.Repositories.Interfaces;

public interface IPlayerRepository
{
    public Task<Player?> GetPlayerAsync(string id);
    Task<bool> IsPlayerExist(string id);
    public Task UpdatePlayerAsync(string id, Player player);
}