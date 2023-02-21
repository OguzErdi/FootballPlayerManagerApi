using System.Net;
using FootballPlayerManagerApi.Constants;
using FootballPlayerManagerApi.Contracts;
using FootballPlayerManagerApi.Contracts.Request;
using FootballPlayerManagerApi.Entities;
using FootballPlayerManagerApi.Helpers;
using FootballPlayerManagerApi.Services.Interfaces;
using FootballPlayerManagerApi.Validators;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FootballPlayerManagerApi.Controllers;

[ApiController]
[Route("[controller]/{id}")]
public class PlayersController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayersController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpGet, Route("")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Get player", typeof(ServiceResponse<Player>))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Return not found if player not found",
        typeof(ServiceResponse<Player>))]
    public async Task<IActionResult> GetPlayer(string id)
    {
        var response = await _playerService.GetPlayerAsync(id);

        if (response.ErrorMessage == ErrorMessages.PlayerNotFound) return NotFound(response);

        return Ok(response);
    }

    [HttpPut, Route("")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Update player", typeof(ServiceResponse<bool>))]
    [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity,
        "Return unprocessable entity if request have validation errors", typeof(ServiceResponse<bool>))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Return not found if player not found",
        typeof(ServiceResponse<bool>))]
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