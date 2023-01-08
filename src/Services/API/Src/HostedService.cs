using System.Threading;
using System.Threading.Tasks;
using Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using Processing.Listeners;
using Processing.Workers;

namespace API
{
    internal class HostedService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        private readonly ILogger _logger = LogManager.GetLogger(nameof(HostedService));

        public HostedService(IServiceScopeFactory factory)
        {
            _scopeFactory = factory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await MigrateAsync(cancellationToken);
            StartWorkers();
            StartListeners();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            StopWorkers();
            StopListeners();
        }

        private async Task MigrateAsync(CancellationToken cancellationToken)
        {
            _logger.Info("Start migrations...");

            using var scope = _scopeFactory.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            var isCreated = await db.Database.EnsureCreatedAsync(cancellationToken);

            _logger.Info(isCreated ? "Migrations done" : "Skip migrations: database already exists");
        }

        private void StartWorkers()
        {
            using var scope = _scopeFactory.CreateScope();

            var worker = scope.ServiceProvider.GetService<TestsWorker>();

            worker.Start();
        }

        private void StopWorkers()
        {
            using var scope = _scopeFactory.CreateScope();

            var worker = scope.ServiceProvider.GetService<TestsWorker>();

            worker.Stop();
        }

        private void StartListeners()
        {
            using var scope = _scopeFactory.CreateScope();

            var listener = scope.ServiceProvider.GetService<SolutionsListener>();

            listener.Start();
        }

        private void StopListeners()
        {
            using var scope = _scopeFactory.CreateScope();

            var listener = scope.ServiceProvider.GetService<SolutionsListener>();

            listener.Stop();
        }
    }
}
