using System;
using System.Collections.Generic;

namespace SFInput {
public interface IInputControllerManager<TController> where TController : IInputController
{
    IReadOnlyDictionary<int, IReadOnlyList<TController>> Controllers { get; }
    IReadOnlyDictionary<int, IInputPredicateManager> PredicateManagers { get; }

    event Action<int, TController> ControllerAdded;
    event Action<int, TController> ControllerRemoved;
    event Action<int, IInputPredicateManager> PredicateManagerAdded;
    event Action<int, IInputPredicateManager> PredicateManagerRemoved;

    void AddController(int index, TController controller);
    void AddPredicateManager(int index, IInputPredicateManager manager);

    void RemoveController(int index, TController controller);
    void RemovePredicateManager(int index);
}}