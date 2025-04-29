namespace IUInput {
public interface IInputPredicateManager<T> : IInternalInputPredicateManager<IInputPredicate<T>>
{
    bool AllResult(T entry);
}}