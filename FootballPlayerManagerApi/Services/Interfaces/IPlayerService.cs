using FootballPlayerManagerApi.Services.PlayersService;

namespace FootballPlayerManagerApi.Services.Interfaces;

public interface IPlayerService
{
    public Player GetPlayer(string id);

    public bool UpdatePlayer(string id);
}