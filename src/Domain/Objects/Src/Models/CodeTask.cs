using System;

namespace Objects.Models
{
    public class CodeTask
    {
        public ulong Id { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public DateTime CreatedUtc { get; set; }

        public DateTime UpdatedUtc { get; set; }
    }
}
