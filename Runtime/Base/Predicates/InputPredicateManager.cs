using System.Collections.Generic;

namespace IUInput {
public sealed class InputPredicateManager : ListHandler<IInputPredicate>, IInputPredicateManager
{
    public IReadOnlyList<IInputPredicate> Predicates { get => _source; }

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