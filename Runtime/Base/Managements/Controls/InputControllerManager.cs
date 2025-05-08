using System;
using System.Collections.Generic;

namespace IUInput {
public sealed class InputControllerManager<TController> : IInputControllerManager<TController> where TController : IInputController
{
    private readonly Dictionary<int, IReadOnlyList<TController>> _controllers;
    public IReadOnlyDictionary<int, IReadOnlyList<TController>> Controllers { get => _controllers; }

    public event Action<int, TController> ControllerAdded;
    public event Action<int, TController> ControllerRemoved;

    public InputControllerManager()
    {
        _controllers = new();
    }

    public bool AddController(int key, TController controller)
    {
        if (controller is null) return false;

        if (_controllers.ContainsKey(key) is false)
        {
            var collection = new List<TController>();
            _controllers.Add(key, collection);
        }

        ((ICollection<TController>)_controllers[key]).Add(controller);
        ControllerAdded?.Invoke(key, controller);

        return true;
    }

    public bool RemoveController(int key, TController controller)
    {
        if (_controllers.TryGetValue(key, out var collection))
        {
            if (((ICollection<TController>)collection).Remove(controller))
            {
                ControllerRemoved?.Invoke(key, controller);
                return true;
            }
        }

        return false;
    }
}}