using Couchbase;
using Couchbase.KeyValue;

namespace FootballPlayerManagerApi.Couchbase.Providers.Interfaces;

public interface IFootballProvider
{
    Task<ICouchbaseCollection> GetCollection(string collectionName);
    Task<IScope> GetScope();
    Task<IBucket> GetBucket();
    Task<ICluster> GetCluster();
}