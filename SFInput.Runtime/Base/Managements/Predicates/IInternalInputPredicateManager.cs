using System;
using System.Collections.Generic;

namespace SFInput {
public interface IInternalInputPredicateManager<T>
{
    IEnumerable<T> Predicates { get; }

    event Action<T> PredicateAdded;
    event Action<T> PredicateRemoved;

    bool AddPredicate(T predicate);
    bool RemovePredicate(T predicate);
}}