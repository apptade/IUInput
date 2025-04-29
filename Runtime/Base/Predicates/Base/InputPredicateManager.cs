namespace IUInput {
public sealed class InputPredicateManager : InternalInputPredicateManager<IInputPredicate>, IInputPredicateManager
{
    public bool AllResult()
    {
        foreach (var predicate in Predicates)
        {
            if (predicate.Result() is false)
                return false;
        }

        return true;
    }
}}