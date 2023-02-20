using FootballPlayerManagerApi.Contracts;

namespace FootballPlayerManagerApi.Constants;

public record ErrorMessages
{
    public static ErrorMessage PlayerNotFound => new()
    {
        Code = "PlayerNotFound",
        Message = "Player not found"
    };
    
    public static ErrorMessage NameIsEmpty => new()
    {
        Code = "FieldIsEmpty",
        Message = "Name must be given"
    };
    
    public static ErrorMessage HeightIsEmpty => new()
    {
        Code = "HeightIsEmpty",
        Message = "Height must be given"
    };
    
    public static ErrorMessage HeightNotValid => new()
    {
        Code = "HeightNotValid",
        Message = "Height is must range from 0 to 400 cm"
    };
    
    public static ErrorMessage AgeIsEmpty => new()
    {
        Code = "AgeIsEmpty",
        Message = "Age must be given"
    };
    
    public static ErrorMessage AgeNotValid => new()
    {
        Code = "AgeNotValid",
        Message = "Age is must range from 0 to 150 years"
    };
}