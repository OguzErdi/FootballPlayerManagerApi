using Couchbase;
using Couchbase.KeyValue;

namespace FootballPlayerManagerApi.CouchbaseProviders.Interfaces;

public interface IFootballBucketProvider
{
    Task<ICouchbaseCollection> GetCollection(string collectionName);
    Task<IScope> GetScope();
    Task<IBucket> GetBucket();
    Task<ICluster> GetCluster();
}