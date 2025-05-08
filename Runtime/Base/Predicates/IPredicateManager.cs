using System;
using System.Collections.Generic;

namespace IUInput {
public interface IPredicateManager
{
    IEnumerable<IPredicate> Predicates { get; }

    event Action<IPredicate> PredicateAdded;
    event Action<IPredicate> PredicateRemoved;

    bool AllResult();
    bool AddPredicate(IPredicate predicate);
    bool RemovePredicate(IPredicate predicate);
}}