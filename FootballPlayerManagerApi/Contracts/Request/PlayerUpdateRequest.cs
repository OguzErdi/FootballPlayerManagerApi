namespace FootballPlayerManagerApi.Contracts.Request;

public record PlayerUpdateRequest
{
    public string Name { get; set; }
    // in cm
    public int Height { get; set; }
    public int Age { get; set; }
}