using Couchbase.KeyValue;
using FootballPlayerManagerApi.CouchbaseProviders.Interfaces;
using FootballPlayerManagerApi.Repositories.Interfaces;
using FootballPlayerManagerApi.Services.PlayersService;

namespace FootballPlayerManagerApi.Repositories.Implementations;

public class PlayerRepository : IPlayerRepository
{
    private const string PlayerCollectionName = "player-collection";
    private readonly IFootballBucketProvider _footballBucketProvider;

    public PlayerRepository(IFootballBucketProvider footballBucketProvider)
    {
        _footballBucketProvider = footballBucketProvider;
    }

    public async Task<Player?> GetPlayerAsync(string id)
    {
        var collection = await GetCollection();
        var getResult = await collection.GetAsync(id);

        return getResult.ContentAs<Player>();
    }

    public bool UpdatePlayerAsync(string id)
    {
        throw new NotImplementedException();
    }

    private async Task<ICouchbaseCollection> GetCollection()
    {
        return await _footballBucketProvider.GetCollection(PlayerCollectionName);
    }
}