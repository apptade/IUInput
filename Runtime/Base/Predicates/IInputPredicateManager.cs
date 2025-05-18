using System.Collections.Generic;

namespace IUInput {
public interface IInputPredicateManager : IListHandler<IInputPredicate>
{
    IReadOnlyList<IInputPredicate> Predicates { get; }
    bool AllResult();
}}