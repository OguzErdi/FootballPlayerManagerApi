using Couchbase;

namespace FootballPlayerManagerApi.Couchbase.Providers.Interfaces;

public interface ICouchbaseProvider
{
    Task<ICluster> GetCluster();
}