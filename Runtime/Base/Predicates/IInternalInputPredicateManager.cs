using System;
using System.Collections.Generic;

namespace IUInput {
public interface IInternalInputPredicateManager<T>
{
    IEnumerable<T> Predicates { get; }

    event Action<T> PredicateAdded;
    event Action<T> PredicateRemoved;

    bool AddPredicate(T predicate);
    bool RemovePredicate(T predicate);
}}