using System.Threading;
using System.Threading.Tasks;
using Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Processing.Listeners;
using Processing.Workers;

namespace API
{
    internal class HostedService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

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
            StartListeners();
        }

        private async Task MigrateAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            await db.Database.EnsureCreatedAsync(cancellationToken);
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
