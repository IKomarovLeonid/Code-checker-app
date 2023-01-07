using System.Collections.Generic;

namespace Processing.Queries
{
    public class SelectResult<T> : BaseResult
    {
        public ICollection<T> Items;

        public string Error;

        public static SelectResult<T> Fetched(ICollection<T> collection)
        {
            return new SelectResult<T>()
            {
                Items = collection,
                State = OperationState.Ok
            };
        }

        public static SelectResult<T> Failed(string error, OperationState state)
        {
            return new SelectResult<T>()
            {
                Error = error,
                State = state
            };
        }
    }
}
