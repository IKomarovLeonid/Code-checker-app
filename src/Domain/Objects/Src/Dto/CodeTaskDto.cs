using System;

namespace Objects.Dto
{
    public class CodeTaskDto
    {
        public ulong Id { get; set; }

        public ulong UserId { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime CreatedUtc { get; set; }

        public DateTime UpdatedUtc { get; set; }
    }
}
