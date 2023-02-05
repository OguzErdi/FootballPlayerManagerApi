using FootballPlayerManagerApi.Services.PlayersService;

namespace FootballPlayerManagerApi.Repositories.Interfaces;

public interface IPlayerRepository
{
    public Player GetPlayer(string id);

    public bool UpdatePlayer(string id);
}