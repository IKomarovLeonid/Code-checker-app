namespace Database
{
    public class StateEvent<TModel> where TModel: class
    {
        public TModel Data { get; private init; }

        public StateEventType Type { get; private init; }

        private StateEvent() { }

        public static StateEvent<TModel> Create(TModel aggregate) => new StateEvent<TModel>()
        {
            Data = aggregate,
            Type = StateEventType.Create
        };

        public static StateEvent<TModel> Update(TModel aggregate) => new StateEvent<TModel>()
        {
            Data = aggregate,
            Type = StateEventType.Update
        };

        public static StateEvent<TModel> Remove(TModel aggregate) => new StateEvent<TModel>()
        {
            Data = aggregate,
            Type = StateEventType.Delete
        };
    }

    public enum StateEventType
    {
        Create,
        Update,
        Delete
    }
}
