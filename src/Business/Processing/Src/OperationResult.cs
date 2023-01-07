namespace Processing
{
    public class OperationResult : BaseResult
    {
        public string Error { get; private set; }

        public ulong? AffectedId { get; private set; }

        public static OperationResult Applied(ulong id)
        {
            return new OperationResult()
            {
                AffectedId = id,
                State = OperationState.Ok
            };
        }

        public static OperationResult Failed(string error, OperationState state)
        {
            return new OperationResult()
            {
                Error = error,
                State = state
            };
        }
    }
}
