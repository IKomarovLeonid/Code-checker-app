using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Objects.Dto;

namespace Database
{
    public interface IStorage<TModel> where TModel : class, IDto
    {
        Task<TModel> AddAsync(TModel model, CancellationToken token);

        Task<TModel> UpdateAsync(TModel model, CancellationToken token);

        Task<ICollection<TModel>> GetAllAsync(CancellationToken token, Expression<Func<TModel, bool>> query = null);

        Task<TModel> FindByIdAsync(ulong id, CancellationToken token);

        IDisposable Subscribe(Action<StateEvent<TModel>> subscriber);
    }
}
