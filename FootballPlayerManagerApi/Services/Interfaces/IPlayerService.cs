using FootballPlayerManagerApi.Contracts;
using FootballPlayerManagerApi.Entities;

namespace FootballPlayerManagerApi.Services.Interfaces;

public interface IPlayerService
{
    public Task<ServiceResponse<Player>> GetPlayerAsync(string id);

    public bool UpdatePlayerAsync(string id);
}