using MediatR;

namespace Processing.Commands
{
    public class CreateSolutionCommand : IRequest<OperationResult>
    {
        public ulong TaskId { get; set; }
        public string Code { get; set; }

        public ulong UserId { get; set; }
    }
}
