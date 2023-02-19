using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using FootballPlayerManagerApi.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace FootballPlayerManagerApi.IntegrationTests;

public class APIWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    private CouchbaseTestcontainer _couchbaseTestContainer;
    private IServiceProvider _serviceProvider;

    public APIWebApplicationFactory()
    {
        CreateCouchbaseContainer();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings-test.json").Build();

            configuration["CouchbaseHost"] = _couchbaseTestContainer.ConnectionString;
            configuration["CouchbaseUsername"] = _couchbaseTestContainer.Username;
            configuration["CouchbasePassword"] = _couchbaseTestContainer.Password;
            configuration["CouchbaseUIPort"] = _couchbaseTestContainer.Port.ToString();

            services.AddSingleton(configuration);
            services.AddSingleton<IPlayerBucketHelper, PlayerBucketHelper>();
            
            // var serviceProvider = services.BuildServiceProvider();
            // using var scope = serviceProvider.CreateScope();
            // _serviceProvider = scope.ServiceProvider;
        });

        builder.UseEnvironment("Development");
    }

    public TType GetRequiredService<TType>()
    {
        return _serviceProvider.GetRequiredService<TType>();
    }

    private void CreateCouchbaseContainer()
    {
        _couchbaseTestContainer = new TestcontainersBuilder<CouchbaseTestcontainer>()
            .WithDatabase(new CouchbaseTestcontainerConfiguration
            {
                Username = "Administrator",
                Password = "password",
                BucketName = "Documents"
            })
            .WithImage("couchbase:enterprise-7.1.3")
            .WithName("CouchbaseIntegrationTestContainer")
            .WithExposedPort(8091)
            .WithExposedPort(8093)
            .WithExposedPort(11210)
            .WithPortBinding(8091, 8091)
            .WithPortBinding(8093, 8093)
            .WithPortBinding(11210, 11210)
            .Build();

        _couchbaseTestContainer.StartAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        _couchbaseTestContainer.CreateBucket("football-bucket");
    }
}