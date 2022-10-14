using FluentValidation;
using WorkflowApi.DataTransferObject;

namespace WorkflowApi.Helpers.FluentValidation
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);
        }
    }
}
