using Couchbase;
using Couchbase.Linq;
using FootballPlayerManagerApi.ConfigOptions;
using FootballPlayerManagerApi.CouchbaseProviders.Interfaces;
using Microsoft.Extensions.Options;

namespace FootballPlayerManagerApi.CouchbaseProviders.Implementations;

public class CouchbaseProvider : ICouchbaseProvider
{
    private ICluster _cluster;
    private readonly CouchbaseOptions _couchbaseOptions;

    public CouchbaseProvider(IOptions<CouchbaseOptions> couchbaseOptions)
    {
        _couchbaseOptions = couchbaseOptions.Value;
    }
        
    public async Task<ICluster> GetCluster()
    {
        if (_cluster != null) return _cluster;

        var hosts = _couchbaseOptions.CouchbaseHost;
        var username = _couchbaseOptions.CouchbaseUsername;
        var password = _couchbaseOptions.CouchbasePassword;
        var uiPort = _couchbaseOptions.CouchbaseUIPort;
            
        var clusterOptions = new ClusterOptions
        {
            UserName = username, Password = password, BootstrapHttpPort = uiPort
        };
        clusterOptions.AddLinq();

        _cluster = await Cluster.ConnectAsync($"{hosts}", clusterOptions);

        return _cluster;
    }
}