using FootballPlayerManagerApi.Contracts;
using FootballPlayerManagerApi.Contracts.Request;
using FootballPlayerManagerApi.Entities;

namespace FootballPlayerManagerApi.Services.Interfaces;

public interface IPlayerService
{
    public Task<ServiceResponse<Player>> GetPlayerAsync(string id);

    public Task<ServiceResponse<bool>> UpdatePlayerAsync(string id, PlayerUpdateRequest request);
}