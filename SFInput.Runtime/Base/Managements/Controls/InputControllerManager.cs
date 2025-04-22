using System;
using System.Collections.Generic;

namespace SFInput {
public sealed class InputControllerManager<TController> : IInputControllerManager<TController> where TController : IInputController
{
    private readonly Dictionary<int, IReadOnlyList<TController>> _controllers;
    private readonly InputControllerPredicateConnector<TController> _connector;

    public IReadOnlyDictionary<int, IReadOnlyList<TController>> Controllers { get => _controllers; }
    public IInputPredicateManager<TController> PredicateManager { get; }

    public event Action<int, TController> ControllerAdded;
    public event Action<int, TController> ControllerRemoved;

    public InputControllerManager()
    {
        _controllers = new();
        _connector = new(this);
        PredicateManager = new InputPredicateManager<TController>();

        _connector.Enable();
    }

    public bool AddController(int index, TController controller)
    {
        if (controller is null) return false;

        if (_controllers.ContainsKey(index) is false)
        {
            var collection = new List<TController>();
            _controllers.Add(index, collection);
        }

        ((ICollection<TController>)_controllers[index]).Add(controller);
        ControllerAdded?.Invoke(index, controller);

        return true;
    }

    public bool RemoveController(int index, TController controller)
    {
        if (_controllers.TryGetValue(index, out var collection))
        {
            if (((ICollection<TController>)collection).Remove(controller))
            {
                ControllerRemoved?.Invoke(index, controller);
                return true;
            }
        }

        return false;
    }
}}