using FluentValidation.Results;
using FootballPlayerManagerApi.Contracts;

namespace FootballPlayerManagerApi.Helpers;

public static class ServiceResponseHelper
{
    public static ServiceResponse<T> CreateServiceResponseWithValidationResult<T>(ValidationResult validationResult)
    {
        var validationError = validationResult.Errors.FirstOrDefault();
        var errorMessage = CreateErrorMessage(validationError?.ErrorMessage, validationError?.ErrorCode);
        var baseResponse = CreateBaseResponseWithErrorMessage<T>(errorMessage);
        return baseResponse;
    }
    
    private static ServiceResponse<T> CreateBaseResponseWithErrorMessage<T>(ErrorMessage errorMessage)
    {
        return new ServiceResponse<T>
        {
            ErrorMessage = errorMessage
        };
    }

    private static ErrorMessage CreateErrorMessage(string message, string code)
    {
        return new ErrorMessage
        {
            Message = message,
            Code = code
        };
    }
}