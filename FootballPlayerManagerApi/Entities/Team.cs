namespace FootballPlayerManagerApi.Entities;

public record Team
{
    public string Name { get; set; }
    public List<Player> Players { get; set; }
}