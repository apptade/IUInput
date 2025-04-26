namespace AFUInput {
public interface IInputPredicateManager<T> : IInternalInputPredicateManager<IInputPredicate<T>>
{
    bool AllResult(T entry);
}}