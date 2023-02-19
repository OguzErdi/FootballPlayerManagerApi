namespace FootballPlayerManagerApi.Contracts;

public record ErrorMessage
{
    public string Code { get; set; }
    public string Message { get; set; }
}