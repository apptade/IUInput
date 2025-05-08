using System;
using System.Collections.Generic;

namespace IUInput {
public sealed class PredicateManager : IPredicateManager
{
    private readonly ICollection<IPredicate> _predicates;
    public IEnumerable<IPredicate> Predicates { get => _predicates; }

    public event Action<IPredicate> PredicateAdded;
    public event Action<IPredicate> PredicateRemoved;

    public PredicateManager()
    {
        _predicates = new List<IPredicate>();
    }

    public bool AllResult()
    {
        foreach (var predicate in Predicates)
        {
            if (predicate.Result() is false)
                return false;
        }

        return true;
    }

    public bool AddPredicate(IPredicate predicate)
    {
        if (predicate is null) return false;

        _predicates.Add(predicate);
        PredicateAdded?.Invoke(predicate);

        return true;
    }

    public bool RemovePredicate(IPredicate predicate)
    {
        if (_predicates.Remove(predicate))
        {
            PredicateRemoved?.Invoke(predicate);
            return true;
        }

        return false;
    }
}}