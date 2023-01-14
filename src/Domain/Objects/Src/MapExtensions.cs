using Objects.Dto;
using Objects.Models;

namespace Objects
{
    public static class MapExtensions
    {
        public static CodeSolution ToModel(this CodeSolutionDto dto) => new CodeSolution()
        {
            Code = dto.Code,
            Id = dto.Id,
            TaskId = dto.TaskId,
            Status = dto.Status,
            PassedTests = dto.PassedCount,
            TotalTests = dto.TestsCount,
            UserId = dto.UserId,
            Errors = dto.Errors,
            CreatedUtc = dto.CreatedUtc,
            UpdatedUtc = dto.UpdatedUtc
        };

        public static CodeTask ToModel(this CodeTaskDto dto) => new CodeTask()
        {
            CreatedUtc = dto.CreatedUtc,
            UpdatedUtc = dto.UpdatedUtc,
            Id = dto.Id,
            Description = dto.Description,
            Title = dto.Title
        };
    }
}
