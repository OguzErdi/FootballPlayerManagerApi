using FootballPlayerManagerApi.Couchbase.Providers.Interfaces;
using FootballPlayerManagerApi.Repositories.Implementations;

namespace FootballPlayerManagerApi.IntegrationTests.Helpers;

public class PlayerBucketHelper : IPlayerBucketHelper
{
    private readonly IFootballProvider _footballProvider;

    public PlayerBucketHelper(IFootballProvider footballProvider)
    {
        _footballProvider = footballProvider;
    }

    public async Task Insert<T>(T data, string id)
    {
        var collection = await _footballProvider.GetCollection(PlayerRepository.PlayerCollectionName);
        await collection.InsertAsync(id, data);
    }
}