using AutoFixture;
using FluentAssertions;
using FootballPlayerManagerApi.Constants;
using FootballPlayerManagerApi.Entities;
using FootballPlayerManagerApi.Repositories.Interfaces;
using FootballPlayerManagerApi.Services.Implementations;
using Moq;

namespace FootballPlayerManagerApi.UnitTests.Services;

public class PlayerServiceTests
{
    private Fixture _fixture;
    private Mock<IPlayerRepository> _playerRepository;
    private PlayerService _sut;

    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture();
        _playerRepository = new Mock<IPlayerRepository>();
        
        _sut = new PlayerService(_playerRepository.Object);
    }

    [Test]
    public async Task GetPlayerAsync_TrueStory()
    {
        //Arrange
        var id = _fixture.Create<string>();
        var player = _fixture.Create<Player>();
        _playerRepository.Setup(x => x.GetPlayerAsync(It.IsAny<string>())).ReturnsAsync(player);

        //Act
        var response = await _sut.GetPlayerAsync(id);

        //Verify
        response.Data.Should().Be(player);
    }
    
    [Test]
    public async Task GetPlayerAsync_WhenPlayerNotFound_ShouldReturnError()
    {
        //Arrange
        var id = _fixture.Create<string>();

        //Act
        var response = await _sut.GetPlayerAsync(id);

        //Verify
        response.ErrorMessage.Should().Be(ErrorMessages.PlayerNotFound);
    }
}