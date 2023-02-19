using Couchbase.Core.Exceptions.KeyValue;
using Couchbase.KeyValue;
using FootballPlayerManagerApi.Couchbase.Providers.Interfaces;
using FootballPlayerManagerApi.Entities;
using FootballPlayerManagerApi.Repositories.Interfaces;

namespace FootballPlayerManagerApi.Repositories.Implementations;

public class PlayerRepository : IPlayerRepository
{
    public const string PlayerCollectionName = "player-collection";
    private readonly IFootballProvider _footballProvider;

    public PlayerRepository(IFootballProvider footballProvider)
    {
        _footballProvider = footballProvider;
    }

    public async Task<Player?> GetPlayerAsync(string id)
    {
        var collection = await GetCollection();
        try
        {
            var getResult = await collection.GetAsync(id);
            return getResult.ContentAs<Player>();
        }
        catch (DocumentNotFoundException)
        {
            return null;
        }
    }

    public bool UpdatePlayerAsync(string id)
    {
        throw new NotImplementedException();
    }

    private async Task<ICouchbaseCollection> GetCollection()
    {
        return await _footballProvider.GetCollection(PlayerCollectionName);
    }
}