using MediatR;

namespace Processing.Commands
{
    public class CreateTaskCommand : IRequest<OperationResult>
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public string MethodName { get; set; }

        public string NamespaceName { get; set; }
    }
}
