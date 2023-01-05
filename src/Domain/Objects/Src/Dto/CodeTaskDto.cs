using System;

namespace Objects.Dto
{
    internal class CodeTaskDto
    {
        public ulong Id { get; set; }

        public ulong UserId { get; set; }

        public string Code { get; set; }

        public CompilationStatus Compilation { get; set; }

        public TestingStatus Status { get; set; }

        public DateTime CreatedUtc { get; set; }

        public DateTime UpdatedUtc { get; set; }
    }
}
