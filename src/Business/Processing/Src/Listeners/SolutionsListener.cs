using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Database;
using NLog;
using Objects;
using Objects.Dto;
using Processing.Parsing;
using Processing.Workers;

namespace Processing.Listeners
{
    public class SolutionsListener 
    {
        private readonly IStorage<CodeSolutionDto> _storage;
        private readonly IStorage<CodeTaskDto> _tasks;
        private readonly CodeParser _parser;
        private readonly IWorker _worker;

        private readonly ILogger _logger = LogManager.GetLogger(nameof(SolutionsListener));

        private IDisposable _subscription;

        public SolutionsListener(IStorage<CodeSolutionDto> storage, IStorage<CodeTaskDto> tasks, CodeParser parser, TestsWorker worker)
        {
            _storage = storage;
            _tasks = tasks;
            _parser = parser;
            _worker = worker;
        }

        public void Start()
        {
            _logger.Info("Start listener...");
            _subscription = _storage.Subscribe(ProcessEvents);
            _logger.Info("Listener has been started");
        }

        public void Stop()
        {
            _logger.Info("Stop listener...");
            _subscription?.Dispose();

            _logger.Info("Listener has been stopped");
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }

        private void ProcessEvents(StateEvent<CodeSolutionDto> stateEvent)
        {
            if(stateEvent.Type == StateEventType.Create)
            {
                OnCreateAsync(stateEvent.Data);
            }
        }

        private async Task OnCreateAsync(CodeSolutionDto dto)
        {
            var cts = CancellationToken.None;

            // find task
            var task = await _tasks.FindByIdAsync(dto.TaskId, cts);

            if (task == null)
            {
                _logger.Error($"Failed to get task by id '{dto.TaskId}' from storage");

                dto.Errors = $"Failed to get task by id '{dto.TaskId}'";
                await _storage.UpdateAsync(dto, cts);
                return;
            }

            var parsingResult = _parser.Parse(task.NamespaceName, task.MethodName, dto.Code);

            if (!parsingResult.IsSuccess)
            {
                var errorsAll = string.Join(System.Environment.NewLine, parsingResult.Errors.ToArray());
                dto.Errors = errorsAll;
                dto.Status = SolutionStatus.CompilationError;
                await _storage.UpdateAsync(dto, cts);
                return;
            }

            var model = dto.ToModel();
            model.MethodInfo = parsingResult.Info;
            model.MethodName = parsingResult.MethodToCall;
            model.NamespaceName = parsingResult.Namespace;

            _worker.Push(model);
        }
    }
}
