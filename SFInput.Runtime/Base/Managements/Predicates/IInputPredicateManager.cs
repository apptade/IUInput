using System;

namespace SFInput {
public interface IInputPredicateManager
{
    event Action<IInputPredicateManager> ManagerAdded;
    event Action<IInputPredicateManager> ManagerRemoved;
    event Action<IInputPredicate> PredicateAdded;
    event Action<IInputPredicate> PredicateRemoved;

    void AddManager(IInputPredicateManager manager);
    void AddPredicate(IInputPredicate predicate);

    void RemoveManager(IInputPredicateManager manager);
    void RemovePredicate(IInputPredicate predicate);

    bool Result();
}}