using MediatR;

namespace Processing.Queries
{
    public class FindQuery<TModel> : IRequest<FindResult<TModel>>
    {
        public ulong Id { get; }

        public FindQuery(ulong id)
        {
            Id = id;
        }
    }
}
