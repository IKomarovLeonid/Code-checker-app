using System.Collections.Generic;
using MediatR;

namespace Processing.Queries
{
    public class SelectQuery<TModel> : IRequest<SelectResult<TModel>>
    {

    }
}
