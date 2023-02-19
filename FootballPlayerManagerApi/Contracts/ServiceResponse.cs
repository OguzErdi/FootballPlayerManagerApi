namespace FootballPlayerManagerApi.Contracts;

public record ServiceResponse<T>
{
    public bool HasError => ErrorMessage != null;
    public ErrorMessage ErrorMessage { get; set; }
    public T Data { get; set; }
}