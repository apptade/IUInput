namespace SFInput {
public interface IInputPredicateManager
{
    void AddManager(IInputPredicateManager manager);
    void AddPredicate(IInputPredicate predicate);

    void RemoveManager(IInputPredicateManager manager);
    void RemovePredicate(IInputPredicate predicate);

    bool Result();
}}