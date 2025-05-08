using System;
using System.Collections.Generic;

namespace IUInput {
public interface IInputControllerManager<TController> where TController : IInputController
{
    IReadOnlyDictionary<int, IReadOnlyList<TController>> Controllers { get; }

    event Action<int, TController> ControllerAdded;
    event Action<int, TController> ControllerRemoved;

    bool AddController(int key, TController controller);
    bool RemoveController(int key, TController controller);
}}