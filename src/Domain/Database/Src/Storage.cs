using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Objects.Dto;

namespace Database
{
    public class Storage<TModel> : IStorage<TModel> where TModel : class, IDto
    {
        private readonly IServiceScopeFactory _factory;

        private readonly Subject<StateEvent<TModel>> _subject = new Subject<StateEvent<TModel>>();
        private readonly EventLoopScheduler _scheduler = new EventLoopScheduler();

        public Storage(IServiceScopeFactory factory)
        {
            _factory = factory;
        }


        public async Task<TModel> AddAsync(TModel model, CancellationToken token)
        {
            using var scope = _factory.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            var time = DateTime.UtcNow;
            model.CreatedUtc = time;
            model.UpdatedUtc = time;

            var entity = await context.Set<TModel>().AddAsync(model, token);

            await context.SaveChangesAsync(token);

            _subject.OnNext(StateEvent<TModel>.Create(entity.Entity));

            return entity.Entity;
        }

        public async Task<TModel> UpdateAsync(TModel model, CancellationToken token)
        {
            using var scope = _factory.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var entry = await context.Set<TModel>().FirstAsync(t => t.Id == model.Id, token);

            model.UpdatedUtc = DateTime.UtcNow;

            var entryEntry = context.Entry(entry);
            entryEntry.CurrentValues.SetValues(model);
            await context.SaveChangesAsync(token);

            _subject.OnNext(StateEvent<TModel>.Update(entryEntry.Entity));

            return entryEntry.Entity;
        }

        public async Task<ICollection<TModel>> GetAllAsync(CancellationToken token, Expression<Func<TModel, bool>> query = null)
        {
            using var scope = _factory.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var entities = context.Set<TModel>().AsNoTracking();
            if (query != null)
            {
                return await entities.Where(query).ToListAsync(token);
            }

            return await entities.ToListAsync(token);
        }

        public async Task<TModel> FindByIdAsync(ulong id, CancellationToken token)
        {
            using var scope = _factory.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            var entities = context.Set<TModel>().AsNoTracking();

            return await entities.FirstOrDefaultAsync(t => t.Id == id, token);
        }

        public IDisposable Subscribe(Action<StateEvent<TModel>> subscriber)
        {
            return _subject.ObserveOn(_scheduler).Subscribe(subscriber);
        }
    }
}
