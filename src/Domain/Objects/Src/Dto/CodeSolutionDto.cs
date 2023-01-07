using FluentValidation;
using Objects.Validation;
using System;
using FluentValidation.Results;

namespace Objects.Dto
{
    public class CodeSolutionDto : IDto
    {
        public ulong Id { get; set; }

        public ulong TaskId { get; set; }

        public ulong UserId { get; set; }

        public string Code { get; set; }

        public SolutionStatus Status { get; set; }

        public string Errors { get; set; }

        public ulong TestsCount { get; set; }

        public ulong PassedCount { get; set; }

        public DateTime CreatedUtc { get; set; }

        public DateTime UpdatedUtc { get; set; }

        private static readonly IValidator<CodeSolutionDto> Validation = new TaskSolutionValidation();

        public ValidationResult Validate()
        {
            return Validation.Validate(this);
        }
    }
}
