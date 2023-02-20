
namespace FootballPlayerManagerApi.Entities;

public record Player
{
    public string Name { get; set; }
    public int Height { get; set; }
    public int Age { get; set; }
}