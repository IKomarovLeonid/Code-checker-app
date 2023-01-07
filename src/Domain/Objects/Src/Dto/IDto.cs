using System;

namespace Objects.Dto
{
    public interface IDto
    {
        public ulong Id { get; set; }

        public DateTime CreatedUtc { get; set; }

        public DateTime UpdatedUtc { get; set; }
    }
}
