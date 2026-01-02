using System.Collections.Generic;

namespace IUInput {
public interface IInputPredicateManager : IReactiveList<IInputPredicate>
{
    IReadOnlyList<IInputPredicate> Predicates { get; }
    bool AllResult();
}}