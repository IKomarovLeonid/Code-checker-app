using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Database;
using MediatR;
using Objects;
using Objects.Dto;
using Objects.Models;
using Processing.Queries;

namespace Processing.Handlers.Solutions
{
    internal class GetSolutionHandler : IRequestHandler<SelectQuery<CodeSolution>, SelectResult<CodeSolution>>
    {
        private readonly IStorage<CodeSolutionDto> _storage;

        public GetSolutionHandler(IStorage<CodeSolutionDto> storage)
        {
            _storage = storage;
        }

        public async Task<SelectResult<CodeSolution>> Handle(SelectQuery<CodeSolution> request, CancellationToken cancellationToken)
        {
            var items = await _storage.GetAllAsync(cancellationToken);

            return SelectResult<CodeSolution>.Fetched(items.Select(dto => dto.ToModel()).ToList());
        }
    }
}
