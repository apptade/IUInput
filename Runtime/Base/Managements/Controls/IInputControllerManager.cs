using System;
using System.Collections.Generic;

namespace AFUInput {
public interface IInputControllerManager<TController> where TController : IInputController
{
    IReadOnlyDictionary<int, IReadOnlyList<TController>> Controllers { get; }
    IInputPredicateManager<TController> PredicateManager { get; }

    event Action<int, TController> ControllerAdded;
    event Action<int, TController> ControllerRemoved;

    bool AddController(int index, TController controller);
    bool RemoveController(int index, TController controller);
}}