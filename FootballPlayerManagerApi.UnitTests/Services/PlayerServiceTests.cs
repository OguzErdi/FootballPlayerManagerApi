using AutoFixture;
using AutoMapper;
using FluentAssertions;
using FootballPlayerManagerApi.Constants;
using FootballPlayerManagerApi.Contracts.Request;
using FootballPlayerManagerApi.Entities;
using FootballPlayerManagerApi.Repositories.Interfaces;
using FootballPlayerManagerApi.Services.Implementations;
using Moq;

namespace FootballPlayerManagerApi.UnitTests.Services;

public class PlayerServiceTests
{
    private Fixture _fixture;
    private Mock<IPlayerRepository> _playerRepository;
    private Mock<IMapper> _mapper;
    private PlayerService _sut;

    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture();
        _playerRepository = new Mock<IPlayerRepository>();
        _mapper = new Mock<IMapper>();

        _sut = new PlayerService(_playerRepository.Object, _mapper.Object);
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

    [Test]
    public async Task UpdatePlayerAsync_TrueStory()
    {
        //Arrange
        var id = _fixture.Create<string>();
        var request = _fixture.Create<PlayerUpdateRequest>();
        _playerRepository.Setup(x => x.IsPlayerExist(It.IsAny<string>())).ReturnsAsync(true);

        //Act
        var response = await _sut.UpdatePlayerAsync(id, request);

        //Verify
        response.Data.Should().Be(true);
    }

    [Test]
    public async Task UpdatePlayerAsync_WhenPlayerNotFound_ShouldReturnError()
    {
        //Arrange
        var id = _fixture.Create<string>();
        var request = _fixture.Create<PlayerUpdateRequest>();
        _playerRepository.Setup(x => x.IsPlayerExist(It.IsAny<string>())).ReturnsAsync(false);

        //Act
        var response = await _sut.UpdatePlayerAsync(id, request);
        
        //Verify
        response.ErrorMessage.Should().Be(ErrorMessages.PlayerNotFound);
    }
}