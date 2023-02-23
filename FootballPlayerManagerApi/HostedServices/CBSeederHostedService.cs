using FootballPlayerManagerApi.Couchbase.Providers.Implementations;
using FootballPlayerManagerApi.Couchbase.Providers.Interfaces;
using FootballPlayerManagerApi.Entities;

namespace FootballPlayerManagerApi.HostedServices;

public class CBSeederHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public CBSeederHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // Create a new scope to retrieve scoped services
        using (var scope = _serviceProvider.CreateScope())
        {
            // Get the DbContext instance
            var cbProvider = scope.ServiceProvider.GetRequiredService<IFootballProvider>();

            await SeedTeamData(cbProvider);

            await SeedPlayerData(cbProvider);
        }
    }

    private static async Task SeedPlayerData(IFootballProvider cbProvider)
    {
        var playerCollection = await cbProvider.GetCollection("player-collection");
        await playerCollection.UpsertAsync("david_de_gea",
            new Player { Name = "David de Gea", Height = 192, Age = 32 });
        await playerCollection.UpsertAsync("luke_shaw",
            new Player { Name = "Luke Shaw", Height = 150, Age = 27 });
        await playerCollection.UpsertAsync("mason_mount",
            new Player { Name = "Mason Mount", Height = 181, Age = 24 });
        await playerCollection.UpsertAsync("raheem_sterling",
            new Player { Name = "Raheem Sterling", Height = 172, Age = 28 });
    }

    private static async Task SeedTeamData(IFootballProvider cbProvider)
    {
        var teamCollection = await cbProvider.GetCollection("team-collection");
        await teamCollection.UpsertAsync("chelsea_fc", new Team
        {
            Name = "Chelsea FC", PlayerIds = new List<string> { "david_de_gea", "mason_mount" }
        });

        await teamCollection.UpsertAsync("manchester_united", new Team
        {
            Name = "Manchester United FC", PlayerIds = new List<string> { "david_de_gea", "luke_shaw" }
        });
    }

    // noop
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}