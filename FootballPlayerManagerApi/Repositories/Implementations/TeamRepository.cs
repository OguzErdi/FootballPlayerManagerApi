using Couchbase.Core.Exceptions.KeyValue;
using Couchbase.KeyValue;
using FootballPlayerManagerApi.Couchbase.Providers.Interfaces;
using FootballPlayerManagerApi.Entities;
using FootballPlayerManagerApi.Helpers;
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

    public async Task UpdatePlayerIdsOnTeam(string id, List<string> playerIds)
    {
        var collection = await GetCollection();
        await collection.MutateInAsync(id, specs =>
            specs.Replace(nameof(Team.PlayerIds).ToJsonFormat(), playerIds));
    }

    public async Task<List<string>> GetPlayerIdsFromTeam(string id)
    {
        var collection = await GetCollection();
        var playerIdsResult = await collection.LookupInAsync(id, specs =>
            specs.Get(nameof(Team.PlayerIds).ToJsonFormat()));

        var playerIds = playerIdsResult.ContentAs<List<string>>(0);
        return playerIds ?? new List<string>();
    }

    public async Task<Team?> GetTeamAsync(string id)
    {
        var collection = await GetCollection();
        try
        {
            var getResult = await collection.GetAsync(id);
            var team = getResult.ContentAs<Team>();
            return team;
        }
        catch (DocumentNotFoundException)
        {
            return null;
        }
    }

    public async Task<bool> IsTeamExist(string id)
    {
        var collection = await GetCollection();
        var getResult = await collection.ExistsAsync(id);
        return getResult.Exists;
    }

    private async Task<ICouchbaseCollection> GetCollection()
    {
        return await _footballProvider.GetCollection(TeamCollectionName);
    }
}