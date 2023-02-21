using AutoFixture;
using AutoMapper;
using FluentAssertions;
using FootballPlayerManagerApi.Constants;
using FootballPlayerManagerApi.Entities;
using FootballPlayerManagerApi.Repositories.Interfaces;
using FootballPlayerManagerApi.Services.Implementations;
using Moq;

namespace FootballPlayerManagerApi.UnitTests.Services;

public class TeamServiceTests
{
    private Fixture _fixture;
    private Mock<ITeamRepository> _teamRepository;
    private Mock<IPlayerRepository> _playerRepository;
    private Mock<IMapper> _mapper;
    private TeamService _sut;

    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture();
        _playerRepository = new Mock<IPlayerRepository>();
        _teamRepository = new Mock<ITeamRepository>();
        _mapper = new Mock<IMapper>();

        _sut = new TeamService(_teamRepository.Object, _playerRepository.Object, _mapper.Object);
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
        var team = _fixture.Create<Team>();
        var player = _fixture.Create<Player>();
        _teamRepository.Setup(x => x.GetTeamAsync(It.IsAny<string>())).ReturnsAsync(team);
        _playerRepository.Setup(x => x.GetPlayerAsync(It.IsAny<string>())).ReturnsAsync(player);

        //Act
        var response = await _sut.GetTeamsPlayersAsync(id);

        //Verify
        response.Data.Should().BeOfType<List<string>>();
    }
}