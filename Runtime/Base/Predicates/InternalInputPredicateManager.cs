using System;
using System.Collections.Generic;

namespace IUInput {
public abstract class InternalInputPredicateManager<T> : IInternalInputPredicateManager<T>
{
    private readonly ICollection<T> _predicates;
    public IEnumerable<T> Predicates { get => _predicates; }

    public event Action<T> PredicateAdded;
    public event Action<T> PredicateRemoved;

    public InternalInputPredicateManager()
    {
        _predicates = new List<T>();
    }

    public bool AddPredicate(T predicate)
    {
        if (predicate is null) return false;

        _predicates.Add(predicate);
        PredicateAdded?.Invoke(predicate);

        return true;
    }

    public bool RemovePredicate(T predicate)
    {
        if (_predicates.Remove(predicate))
        {
            PredicateRemoved?.Invoke(predicate);
            return true;
        }

        return false;
    }
}}