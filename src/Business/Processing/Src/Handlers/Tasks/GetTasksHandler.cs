using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Database;
using MediatR;
using Objects;
using Objects.Dto;
using Objects.Models;
using Processing.Queries;

namespace Processing.Handlers.Tasks
{
    internal class GetTaskHandler : IRequestHandler<SelectQuery<CodeTask>, SelectResult<CodeTask>>
    {
        private readonly IStorage<CodeTaskDto> _storage;

        public GetTaskHandler(IStorage<CodeTaskDto> storage)
        {
            _storage = storage;
        }

        public async Task<SelectResult<CodeTask>> Handle(SelectQuery<CodeTask> request, CancellationToken cancellationToken)
        {
            var items = await _storage.GetAllAsync(cancellationToken);

            return SelectResult<CodeTask>.Fetched(items.Select(t => t.ToModel()).ToList());
        }
    }
}
