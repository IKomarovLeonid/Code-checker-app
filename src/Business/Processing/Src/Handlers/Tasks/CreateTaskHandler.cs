using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Database;
using MediatR;
using Objects.Dto;
using Processing.Commands;

namespace Processing.Handlers.Tasks
{
    internal class CreateTaskHandler : IRequestHandler<CreateTaskCommand, OperationResult>
    {
        private readonly IStorage<CodeTaskDto> _storage;

        public CreateTaskHandler(IStorage<CodeTaskDto> storage)
        {
            _storage = storage;
        }

        public async Task<OperationResult> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var dto = new CodeTaskDto()
            {
                Description = request.Description,
                Title = request.Title,
                MethodName = request.MethodName,
                NamespaceName = request.NamespaceName
            };

            var validation = dto.Validate();
            if(!validation.IsValid) return OperationResult.Failed(validation.Errors.First().ToString(), OperationState.BadRequest);

            var affected = await _storage.AddAsync(dto, cancellationToken);

            return OperationResult.Applied(affected.Id);
        }
    }
}
