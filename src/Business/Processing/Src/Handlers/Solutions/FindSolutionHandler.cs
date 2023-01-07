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
    internal class FindSolutionHandler : IRequestHandler<FindQuery<CodeSolution>, FindResult<CodeSolution>>
    {
        private readonly IStorage<CodeSolutionDto> _storage;

        public FindSolutionHandler(IStorage<CodeSolutionDto> storage)
        {
            _storage = storage;
        }

        public async Task<FindResult<CodeSolution>> Handle(FindQuery<CodeSolution> request, CancellationToken cancellationToken)
        {
            var dto = await _storage.FindByIdAsync(request.Id, cancellationToken);

            if (dto == null) { return FindResult<CodeSolution>.Failed($"Code solution with id {request.Id} does not exists", OperationState.NotFound);}

            return FindResult<CodeSolution>.Applied(dto.ToModel());
        }
    }
}
