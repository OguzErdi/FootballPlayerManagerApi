using Couchbase.Core.Exceptions.KeyValue;
using Couchbase.KeyValue;
using FootballPlayerManagerApi.Couchbase.Providers.Interfaces;
using FootballPlayerManagerApi.Entities;
using FootballPlayerManagerApi.Repositories.Interfaces;

namespace FootballPlayerManagerApi.Repositories.Implementations;

public class TeamRepository : ITeamRepository
{
    private const string TeamCollectionName = "team-collection";
    private readonly IFootballProvider _footballProvider;

    public TeamRepository(IFootballProvider footballProvider)
    {
        _footballProvider = footballProvider;
    }

    public Task<IEnumerable<Player>> GetTeamsPlayersAsync(string id)
    {
        throw new NotImplementedException();
    }

    public bool AddPlayerToTeamAsync(string id)
    {
        throw new NotImplementedException();
    }

    public bool DeletePlayerFromTeamAsync(object id)
    {
        throw new NotImplementedException();
    }

    public async Task<Team> GetTeamAsync(string id)
    {
        var collection = await GetCollection();
        try
        {
            var getResult = await collection.GetAsync(id);
            return getResult.ContentAs<Team>();
        }
        catch (DocumentNotFoundException)
        {
            return null;
        }
    }

    private async Task<ICouchbaseCollection> GetCollection()
    {
        return await _footballProvider.GetCollection(TeamCollectionName);
    }
}