using FluentValidation;
using FootballPlayerManagerApi.Contracts;

namespace FootballPlayerManagerApi.Helpers
{
    public static class ValidatorErrorMessageHelper
    {
        public static IRuleBuilderOptions<T, TProperty> WithErrorMessage<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule, ErrorMessage errorMessage)
        {
            return rule.WithMessage(errorMessage.Message).WithErrorCode(errorMessage.Code);
        }
    }
}