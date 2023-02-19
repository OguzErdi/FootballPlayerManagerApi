using Couchbase;

namespace FootballPlayerManagerApi.CouchbaseProviders.Interfaces;

public interface ICouchbaseProvider
{
    Task<ICluster> GetCluster();
}