namespace SFInput {
public interface IInputPredicateManager<T> : IInternalInputPredicateManager<IInputPredicate<T>>
{
    bool AllResult(T entry);
}}