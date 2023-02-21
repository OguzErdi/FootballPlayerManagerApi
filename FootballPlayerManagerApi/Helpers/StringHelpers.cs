namespace FootballPlayerManagerApi.Helpers;

public static class StringHelpers
{
    public static string ToJsonFormat(this string value)
    {
        return char.ToLower(value[0]) + value[1..];
    }
}