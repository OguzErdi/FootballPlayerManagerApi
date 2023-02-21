using AutoFixture;
using AutoMapper;
using FluentAssertions;
using FootballPlayerManagerApi.Constants;
using FootballPlayerManagerApi.Entities;
using FootballPlayerManagerApi.Repositories.Interfaces;
using FootballPlayerManagerApi.Services.Implementations;
using Microsoft.Extensions.Logging;
using Moq;

namespace FootballPlayerManagerApi.UnitTests.Services;

public class TeamServiceTests
{
    private Fixture _fixture;
    private Mock<ITeamRepository> _teamRepository;
    private Mock<IPlayerRepository> _playerRepository;
    private Mock<IMapper> _mapper;
    private Mock<ILogger<TeamService>> _logger;
    private TeamService _sut;

    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture();
        _playerRepository = new Mock<IPlayerRepository>();
        _teamRepository = new Mock<ITeamRepository>();
        _mapper = new Mock<IMapper>();
        _logger = new Mock<ILogger<TeamService>>();

        _sut = new TeamService(_teamRepository.Object, _playerRepository.Object, _mapper.Object, _logger.Object);
    }

    [Test]
    public async Task GetTeamsPlayersAsync_WhenTeamNotFound_ShouldReturnError()
    {
        //Arrange
        var id = _fixture.Create<string>();
        _teamRepository.Setup(x => x.GetTeamAsync(It.IsAny<string>())).ReturnsAsync(() => null);

        //Act
        var response = await _sut.GetTeamsPlayersAsync(id);

        //Verify
        response.ErrorMessage.Should().Be(ErrorMessages.TeamNotFound);
    }

    [Test]
    public async Task GetTeamsPlayersAsync_WhenTeamsPlayerIdsIsEmpty_ShouldReturnError()
    {
        //Arrange
        var id = _fixture.Create<string>();
        var team = _fixture.Build<Team>()
            .With(x => x.PlayerIds, new List<string>())
            .Create();

        _teamRepository.Setup(x => x.GetTeamAsync(It.IsAny<string>())).ReturnsAsync(team);

        //Act
        var response = await _sut.GetTeamsPlayersAsync(id);

        //Verify
        response.ErrorMessage.Should().Be(ErrorMessages.TeamNotHavePlayers);
    }

    [Test]
    public async Task GetTeamsPlayersAsync_TrueStory()
    {
        //Arrange
        var id = _fixture.Create<string>();
        var player = _fixture.Create<Player>();
        var team = _fixture.Create<Team>();
        _teamRepository.Setup(x => x.GetTeamAsync(It.IsAny<string>())).ReturnsAsync(team);
        _playerRepository.Setup(x => x.GetPlayerAsync(It.IsAny<string>())).ReturnsAsync(player);

        //Act
        var response = await _sut.GetTeamsPlayersAsync(id);

        //Verify
        response.Data.Should().BeOfType<List<Player>>();
        response.Data.Should().HaveCountGreaterThan(0);
    }

    [Test]
    public async Task AddPlayerToTeamAsync_WhenTeamNotFound_ShouldReturnError()
    {
        //Arrange
        var id = _fixture.Create<string>();
        var playerId = _fixture.Create<string>();
        _teamRepository.Setup(x => x.IsTeamExist(It.IsAny<string>())).ReturnsAsync(false);

        //Act
        var response = await _sut.AddPlayerToTeamAsync(id, playerId);

        //Verify
        response.ErrorMessage.Should().Be(ErrorMessages.TeamNotFound);
    }

    [Test]
    public async Task AddPlayerToTeamAsync_WhenPlayerNotFound_ShouldReturnError()
    {
        //Arrange
        var id = _fixture.Create<string>();
        var playerId = _fixture.Create<string>();
        _teamRepository.Setup(x => x.IsTeamExist(It.IsAny<string>())).ReturnsAsync(true);
        _playerRepository.Setup(x => x.IsPlayerExist(It.IsAny<string>())).ReturnsAsync(false);

        //Act
        var response = await _sut.AddPlayerToTeamAsync(id, playerId);

        //Verify
        response.ErrorMessage.Should().Be(ErrorMessages.PlayerNotFound);
    }
    
    [Test]
    public async Task AddPlayerToTeamAsync_WhenThrowException_ShouldReturnError()
    {
        //Arrange
        var id = _fixture.Create<string>();
        var playerId = _fixture.Create<string>();
        var playerIds = _fixture.Create<List<string>>();
        _teamRepository.Setup(x => x.IsTeamExist(It.IsAny<string>())).ReturnsAsync(true);
        _playerRepository.Setup(x => x.IsPlayerExist(It.IsAny<string>())).ReturnsAsync(true);
        _teamRepository.Setup(x => x.GetPlayerIdsFromTeam(It.IsAny<string>())).ReturnsAsync(playerIds);
        _teamRepository.Setup(x => x.UpdatePlayerIdsOnTeam(It.IsAny<string>(), It.IsAny<List<string>>())).Throws(new Exception());

        //Act
        var response = await _sut.AddPlayerToTeamAsync(id, playerId);

        //Verify
        response.ErrorMessage.Should().Be(ErrorMessages.ProcessFailed);
    }
    
    [Test]
    public async Task AddPlayerToTeamAsync_TrueStory()
    {
        //Arrange
        var id = _fixture.Create<string>();
        var playerId = _fixture.Create<string>();
        var playerIds = _fixture.Create<List<string>>();
        _teamRepository.Setup(x => x.IsTeamExist(It.IsAny<string>())).ReturnsAsync(true);
        _playerRepository.Setup(x => x.IsPlayerExist(It.IsAny<string>())).ReturnsAsync(true);
        _teamRepository.Setup(x => x.GetPlayerIdsFromTeam(It.IsAny<string>())).ReturnsAsync(playerIds);
        _teamRepository.Setup(x => x.UpdatePlayerIdsOnTeam(It.IsAny<string>(), It.IsAny<List<string>>())).Callback(() => {})
            .Returns(Task.CompletedTask);

        //Act
        var response = await _sut.AddPlayerToTeamAsync(id, playerId);

        //Verify
        response.Data.Should().Be(true);
    }
    
    [Test]
    public async Task DeletePlayerFromTeamAsync_WhenTeamNotFound_ShouldReturnError()
    {
        //Arrange
        var id = _fixture.Create<string>();
        var playerId = _fixture.Create<string>();
        _teamRepository.Setup(x => x.IsTeamExist(It.IsAny<string>())).ReturnsAsync(false);

        //Act
        var response = await _sut.DeletePlayerFromTeamAsync(id, playerId);

        //Verify
        response.ErrorMessage.Should().Be(ErrorMessages.TeamNotFound);
    }
    
    [Test]
    public async Task DeletePlayerFromTeamAsync_WhenThrowException_ShouldReturnError()
    {
        //Arrange
        var id = _fixture.Create<string>();
        var playerId = _fixture.Create<string>();
        _teamRepository.Setup(x => x.IsTeamExist(It.IsAny<string>())).ReturnsAsync(true);
        _teamRepository.Setup(x => x.UpdatePlayerIdsOnTeam(It.IsAny<string>(), It.IsAny<List<string>>())).Throws(new Exception());
        //Act
        var response = await _sut.DeletePlayerFromTeamAsync(id, playerId);

        //Verify
        response.ErrorMessage.Should().Be(ErrorMessages.ProcessFailed);
    }
    
    [Test]
    public async Task DeletePlayerFromTeamAsync_TrueStory()
    {
        //Arrange
        var id = _fixture.Create<string>();
        var playerId = _fixture.Create<string>();
        _teamRepository.Setup(x => x.IsTeamExist(It.IsAny<string>())).ReturnsAsync(true);
        _teamRepository.Setup(x => x.UpdatePlayerIdsOnTeam(It.IsAny<string>(), It.IsAny<List<string>>())).Callback(() => {})
            .Returns(Task.CompletedTask);

        //Act
        var response = await _sut.DeletePlayerFromTeamAsync(id, playerId);

        //Verify
        response.Data.Should().Be(true);
    }
}