using FluentValidation;
using Objects.Dto;

namespace Objects.Validation
{
    internal class CodeTaskValidator : AbstractValidator<CodeTaskDto>
    {
        public CodeTaskValidator()
        {
            RuleFor(t => t.Title).NotNull()
                .MinimumLength(5)
                .MaximumLength(32);

            RuleFor(t => t.Description).NotNull()
                .MinimumLength(1)
                .MaximumLength(4096);
        }
    }
}
