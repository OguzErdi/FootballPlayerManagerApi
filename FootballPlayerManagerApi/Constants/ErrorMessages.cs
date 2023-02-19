using FootballPlayerManagerApi.Contracts;

namespace FootballPlayerManagerApi.Constants;

public record ErrorMessages
{
    public static ErrorMessage PlayerNotFound => new()
    {
        Code = "PlayerNotFound",
        Message = "Player Not Found"
    };
}