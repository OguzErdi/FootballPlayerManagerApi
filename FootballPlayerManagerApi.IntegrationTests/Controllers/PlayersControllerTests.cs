using System.Net;
using System.Net.Http.Json;
using AutoFixture;
using FluentAssertions;
using FootballPlayerManagerApi.Contracts;
using FootballPlayerManagerApi.Entities;
using FootballPlayerManagerApi.IntegrationTests.Helpers;
using FootballPlayerManagerApi.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FootballPlayerManagerApi.IntegrationTests.Controllers;

public class PlayersControllerTests
{
    private Fixture _fixture;
    private APIWebApplicationFactory<Program> _factory;
    private HttpClient _client;
    private const string MainRoute = "players";

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _fixture = new Fixture();
        _factory = new APIWebApplicationFactory<Program>();
        _client = _factory.CreateClient();
    }

    [Test]
    public async Task GetPlayer_TrueStory()
    {
        // Arrange
        var id = _fixture.Create<string>();
        var player = _fixture.Create<Player>();
        
        using (var scope = _factory.Services.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<IPlayerBucketHelper>().Insert(player, id);
        }
        

        // Act
        var response = await _client.GetAsync($"/{MainRoute}/{id}");
        var data = await response.Content.ReadFromJsonAsync<ServiceResponse<Player>>();

        // Verify
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        // jsonResult.Result.Should().Be(data);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }
}