namespace FootballPlayerManagerApi.IntegrationTests.Helpers;

public interface IPlayerBucketHelper
{
    Task Insert<T>(T data, string id);
}