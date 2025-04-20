namespace SFInput {
public sealed class InputPredicateManager<T> : InternalInputPredicateManager<IInputPredicate<T>>, IInputPredicateManager<T>
{
    public bool AllResult(T entry)
    {
        foreach (var predicate in Predicates)
        {
            if (predicate.Result(entry) is false)
                return false;
        }

        return true;
    }
}}