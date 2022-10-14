using FluentValidation;
using WorkflowApi.DataTransferObject;

namespace WorkflowApi.Helpers.FluentValidation
{
    public class CreateTeamDtoValidator : AbstractValidator<CreateTeamDto>
    {
        public CreateTeamDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();

        }
    }
}
