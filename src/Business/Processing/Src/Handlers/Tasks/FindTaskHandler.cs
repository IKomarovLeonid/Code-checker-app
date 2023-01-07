using Database;
using MediatR;
using Objects.Dto;
using Objects.Models;
using Processing.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Processing.Handlers.Tasks
{
    internal class FindTaskHandler : IRequestHandler<FindQuery<CodeTask>, FindResult<CodeTask>>
    {
        private readonly IStorage<CodeTaskDto> _storage;

        public FindTaskHandler(IStorage<CodeTaskDto> storage)
        {
            _storage = storage;
        }

        public async Task<FindResult<CodeTask>> Handle(FindQuery<CodeTask> request, CancellationToken cancellationToken)
        {
            var dto = await _storage.FindByIdAsync(request.Id, cancellationToken);

            if (dto == null) { return FindResult<CodeTask>.Failed($"Task with id {request.Id} does not exists", OperationState.NotFound);}

            return FindResult<CodeTask>.Applied(new CodeTask()
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                CreatedUtc = dto.CreatedUtc,
                UpdatedUtc = dto.UpdatedUtc
            });
        }
    }
}
