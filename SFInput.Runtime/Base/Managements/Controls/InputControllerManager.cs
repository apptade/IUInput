using System;
using System.Collections.Generic;

namespace SFInput {
public sealed class InputControllerManager<TController> : IInputControllerManager<TController> where TController : IInputController
{
    private readonly Dictionary<int, IInputPredicateManager> _predicateManagers = new();
    private readonly Dictionary<int, IReadOnlyList<TController>> _readControllers = new();
    private readonly Dictionary<int, ICollection<TController>> _writeControllers = new();

    public IReadOnlyDictionary<int, IReadOnlyList<TController>> Controllers { get => _readControllers; }
    public IReadOnlyDictionary<int, IInputPredicateManager> PredicateManagers { get => _predicateManagers; }

    public event Action<int, TController> ControllerAdded;
    public event Action<int, TController> ControllerRemoved;
    public event Action<int, IInputPredicateManager> PredicateManagerAdded;
    public event Action<int, IInputPredicateManager> PredicateManagerRemoved;

    public void AddController(int index, TController controller)
    {
        if (controller is null) return;

        if (_writeControllers.ContainsKey(index) is false)
        {
            var collection = new List<TController>();

            _writeControllers.Add(index, collection);
            _readControllers.Add(index, collection);
        }

        _writeControllers[index].Add(controller);
        ControllerAdded?.Invoke(index, controller);

        if (_predicateManagers.TryGetValue(index, out var manager))
        {
            //controller.PredicateManager.AddManager(manager);
        }
    }

    public void AddPredicateManager(int index, IInputPredicateManager manager)
    {
        if (manager is null) return;

        if (_predicateManagers.TryAdd(index, manager))
        {
            PredicateManagerAdded?.Invoke(index, manager);

            if (_readControllers.TryGetValue(index, out var controllers))
            {
                foreach (var controller in controllers)
                {
                    //controller.PredicateManager.AddManager(manager);
                }
            }
        }
    }

    public void RemoveController(int index, TController controller)
    {
        if (controller is null) return;

        if (_writeControllers.TryGetValue(index, out var collection))
        {
            if (collection.Remove(controller))
            {
                ControllerRemoved?.Invoke(index, controller);

                if (_predicateManagers.TryGetValue(index, out var manager))
                {
                    //controller.PredicateManager.RemoveManager(manager);
                }
            }
        }
    }

    public void RemovePredicateManager(int index)
    {
        if (_predicateManagers.Remove(index, out var manager))
        {
            PredicateManagerRemoved?.Invoke(index, manager);

            if (_readControllers.TryGetValue(index, out var controllers))
            {
                foreach (var controller in controllers)
                {
                    //controller.PredicateManager.RemoveManager(manager);
                }
            }
        }
    }
}}