using Couchbase;
using Couchbase.KeyValue;
using FootballPlayerManagerApi.CouchbaseProviders.Interfaces;

namespace FootballPlayerManagerApi.CouchbaseProviders.Implementations;

public class FootballBucketProvider : IFootballBucketProvider
{
    private const string FootballBucketName = "football-bucket";
    private readonly ICouchbaseProvider _couchbase;
    private IBucket _bucket;
    private IScope _scope;
    private readonly Dictionary<string, ICouchbaseCollection> _couchbaseCollections = new();

    public FootballBucketProvider(ICouchbaseProvider couchbase)
    {
        _couchbase = couchbase;
    }

    public async Task<ICouchbaseCollection> GetCollection(string collectionName)
    {
        if (_couchbaseCollections.TryGetValue(collectionName, out var collection)) return collection;

        var scope = await GetScope();
        collection = await scope.CollectionAsync(collectionName);
        _couchbaseCollections.TryAdd(collectionName, collection);
        return collection;
    }

    public async Task<IScope> GetScope()
    {
        if (_scope != null) return _scope;

        var bucket = await GetBucket();
        _scope = await bucket.ScopeAsync("_default");

        return _scope;
    }

    public async Task<IBucket> GetBucket()
    {
        if (_bucket != null) return _bucket;

        var cluster = await _couchbase.GetCluster();
        _bucket = await cluster.BucketAsync(FootballBucketName);

        return _bucket;
    }

    public Task<ICluster> GetCluster()
    {
        return _couchbase.GetCluster();
    }
}