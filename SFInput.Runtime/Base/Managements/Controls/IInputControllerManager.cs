using System.Collections.Generic;

namespace SFInput {
public interface IInputControllerManager<TController> where TController : IInputController
{
    IReadOnlyDictionary<int, IReadOnlyList<TController>> Controllers { get; }
    IReadOnlyDictionary<int, IInputPredicateManager> PredicateManagers { get; }

    void AddController(int index, TController controller);
    void AddPredicateManager(int index, IInputPredicateManager manager);

    void RemoveController(int index, TController controller);
    void RemovePredicateManager(int index);
}}