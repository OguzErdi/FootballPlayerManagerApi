namespace FootballPlayerManagerApi.Entities;

public record Team
{
    public string Name { get; set; }
    public List<string> PlayerIds { get; init; } = new();
}