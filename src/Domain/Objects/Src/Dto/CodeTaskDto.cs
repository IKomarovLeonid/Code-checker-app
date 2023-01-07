using FluentValidation;
using System;
using FluentValidation.Results;
using Objects.Validation;

namespace Objects.Dto
{
    public class CodeTaskDto : IDto
    {
        public ulong Id { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public string MethodName { get; set; }

        public string NamespaceName { get; set; }

        public DateTime CreatedUtc { get; set; }

        public DateTime UpdatedUtc { get; set; }

        private static readonly IValidator<CodeTaskDto> Validation = new CodeTaskValidator();

        public ValidationResult Validate()
        {
            return Validation.Validate(this);
        }
    }
}
