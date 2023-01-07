namespace Processing.Queries
{
    public class FindResult<T> : BaseResult
    {
        public T Value { get; private set; }

        public string Error { get; private set; }

        private FindResult() { }

        public static FindResult<T> Applied(T value)
        {
            return new FindResult<T>()
            {
                Value = value,
                State = OperationState.Ok
            };
        }

        public static FindResult<T> Failed(string error, OperationState state)
        {
            return new FindResult<T>()
            {
                Error = error,
                State = state
            };
        }
    }
}
