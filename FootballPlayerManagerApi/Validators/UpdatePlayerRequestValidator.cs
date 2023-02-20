using FluentValidation;
using FootballPlayerManagerApi.Constants;
using FootballPlayerManagerApi.Contracts.Request;
using FootballPlayerManagerApi.Helpers;

namespace FootballPlayerManagerApi.Validators;

public class UpdatePlayerRequestValidator: AbstractValidator<PlayerUpdateRequest>
{
    public UpdatePlayerRequestValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(request => request.Name)
            .NotEmpty()
            .WithErrorMessage(ErrorMessages.NameIsEmpty);
        
        RuleFor(request => request.Height)
            .NotEmpty()
            .WithErrorMessage(ErrorMessages.HeightIsEmpty)
            .GreaterThan(0)
            .WithErrorMessage(ErrorMessages.HeightNotValid)
            .LessThan(400)
            .WithErrorMessage(ErrorMessages.HeightNotValid);
        
        RuleFor(request => request.Age)
            .NotEmpty()
            .WithErrorMessage(ErrorMessages.AgeIsEmpty)
            .GreaterThan(0)
            .WithErrorMessage(ErrorMessages.AgeNotValid)
            .LessThan(150)
            .WithErrorMessage(ErrorMessages.AgeNotValid);
    }
}