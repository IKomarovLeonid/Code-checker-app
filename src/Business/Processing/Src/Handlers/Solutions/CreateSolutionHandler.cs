using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Database;
using MediatR;
using Objects.Dto;
using Processing.Commands;

namespace Processing.Handlers.Solutions
{
    internal class CreateSolutionHandler : IRequestHandler<CreateSolutionCommand, OperationResult>
    {
        private readonly IStorage<CodeSolutionDto> _storage;
        private readonly IStorage<CodeTaskDto> _tasks;

        public CreateSolutionHandler(IStorage<CodeSolutionDto> storage, IStorage<CodeTaskDto> tasksStorage)
        {
            _storage = storage;
            _tasks = tasksStorage;
        }

        public async Task<OperationResult> Handle(CreateSolutionCommand request, CancellationToken cancellationToken)
        {
            var dto = new CodeSolutionDto()
            {
                Code = request.Code,
                UserId = request.UserId,
                TaskId = request.TaskId,
                Status = SolutionStatus.Pending
            };

            var validation = dto.Validate();
            if(!validation.IsValid) return OperationResult.Failed(validation.Errors.First().ToString(), OperationState.BadRequest);

            var taskDto = await _tasks.FindByIdAsync(dto.TaskId, cancellationToken);
            if(taskDto == null) return OperationResult.Failed($"Task {dto.TaskId} does not found", OperationState.NotFound);
            
            var affected = await _storage.AddAsync(dto, cancellationToken);

            return OperationResult.Applied(affected.Id);
        }
    }
}
