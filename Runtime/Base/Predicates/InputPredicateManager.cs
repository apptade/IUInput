using System.Collections.Generic;

namespace IUInput {
public sealed class InputPredicateManager : ReactiveList<IInputPredicate>, IInputPredicateManager
{
    public IReadOnlyList<IInputPredicate> Predicates { get => _items; }

    public bool AllResult()
    {
        foreach (var predicate in _items)
        {
            if (predicate.Result() is false)
                return false;
        }

        return true;
    }
}}