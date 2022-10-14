using FluentValidation;
using WorkflowApi.DataTransferObject;

namespace WorkflowApi.Helpers.FluentValidation
{
    public class CreateAppTaskDtoValidator : AbstractValidator<CreateAppTaskDto>
    {
        public CreateAppTaskDtoValidator()
        {
            RuleFor(x => x.TeamId).NotEmpty();
        }
    }
}
