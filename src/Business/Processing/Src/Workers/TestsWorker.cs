using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Database;
using NLog;
using Objects.Dto;
using Objects.Models;
using Processing.Logic;

namespace Processing.Workers
{
    public class TestsWorker : IWorker
    {
        private readonly TestsRunner _runner;
        private readonly IStorage<CodeSolutionDto> _storage;

        private readonly ILogger _logger = LogManager.GetLogger(nameof(TestsWorker));

        private readonly BlockingCollection<CodeSolution> _solutions = new BlockingCollection<CodeSolution>();
        private Task _processingTask;

        public TestsWorker(TestsRunner runner, IStorage<CodeSolutionDto> storage)
        {
            _runner = runner;
            _storage = storage;
        }

        public void Start()
        {
            _logger.Info("Start worker...");
            _processingTask = Task.Factory.StartNew(Process, TaskCreationOptions.LongRunning);
            _logger.Info("Worker has been started");
        }

        public void Stop()
        {
            _solutions.CompleteAdding();
            _processingTask?.Wait();
        }

        public void Push(CodeSolution solution)
        {
            _solutions.Add(solution);
        }

        private void Process()
        {
            try
            {
                while (!_solutions.IsCompleted)
                {
                    var operation = _solutions.Take();
                    Task.Factory.StartNew(() => ProcessTask(operation));
                }
            }
            catch (InvalidOperationException)
            {
                _logger.Info("Worker has been stopped");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }

        private async Task ProcessTask(CodeSolution solution)
        {
            var result = _runner.Execute(solution);

            var dto = await _storage.FindByIdAsync(solution.Id, CancellationToken.None);

            dto.TestsCount = result.TestsCount;
            dto.PassedCount = result.PassedCount;
            dto.Status = result.TestsCount == result.PassedCount
                ? SolutionStatus.Successful
                : SolutionStatus.NotSuccessful;

            await _storage.UpdateAsync(dto, CancellationToken.None);
        }
        
        public void Dispose()
        {
            _solutions?.CompleteAdding();
            _processingTask?.Wait();
        }
    }
}