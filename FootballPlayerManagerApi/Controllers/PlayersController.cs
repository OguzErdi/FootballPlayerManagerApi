using FluentValidation.Results;
using FootballPlayerManagerApi.Constants;
using FootballPlayerManagerApi.Contracts;
using FootballPlayerManagerApi.Contracts.Request;
using FootballPlayerManagerApi.Helpers;
using FootballPlayerManagerApi.Services.Implementations;
using FootballPlayerManagerApi.Services.Interfaces;
using FootballPlayerManagerApi.Validators;
using Microsoft.AspNetCore.Mvc;

namespace FootballPlayerManagerApi.Controllers;

[ApiController]
[Route("[controller]/{id}")]
public class PlayersController : ControllerBase
{
    private readonly IPlayerService _playerService;
    private readonly ILogger<PlayersController> _logger;

    public PlayersController(ILogger<PlayersController> logger, IPlayerService playerService)
    {
        _logger = logger;
        _playerService = playerService;
    }

    [HttpGet, Route("")]
    public async Task<IActionResult> GetPlayer(string id)
    {
        var response = await _playerService.GetPlayerAsync(id);

        if (response.ErrorMessage == ErrorMessages.PlayerNotFound) return NotFound(response);

        return Ok(response);
    }

    [HttpPut, Route("")]
    public async Task<IActionResult> UpdatePlayer(string id, [FromBody] PlayerUpdateRequest request)
    {
        var requestValidator = new UpdatePlayerRequestValidator();
        var validationResult = await requestValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var baseResponse = ServiceResponseHelper.CreateServiceResponseWithValidationResult<bool>(validationResult);
            return UnprocessableEntity(baseResponse);
        }
        
        var response = await _playerService.UpdatePlayerAsync(id, request);

        return response.HasError switch
        {
            true when response.ErrorMessage.Equals(ErrorMessages.PlayerNotFound) => NotFound(response),
            _ => Ok(response)
        };
    }
}