using FluentValidation;
using Objects.Dto;

namespace Objects.Validation
{
    internal class TaskSolutionValidation : AbstractValidator<CodeSolutionDto>
    {
        public TaskSolutionValidation()
        {
            RuleFor(t => t.Code).NotNull()
                .MinimumLength(1)
                .MaximumLength(4096);
        }
    }
}
