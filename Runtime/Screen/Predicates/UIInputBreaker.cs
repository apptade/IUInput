using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace IUInput.Screen {
public abstract class UIInputBreaker<TController, TData> : MonoBehaviour where TController : IInputController where TData : IInputData
{
    [SerializeField] protected InputManager<TController, TData> _addableManager;
    [SerializeField] protected EventSystem _eventSystem;

    private Dictionary<int, Dictionary<TController, UIPredicate<TController>>> _predicates;

    private void Awake()
    {
        _predicates = new();
        AddFirstPredicates();

        _addableManager.ControllerManager.ValueAdded += AddPredicate;
        _addableManager.ControllerManager.ValueRemoved += RemovePredicate;
    }

    private void OnDestroy()
    {
        _addableManager.ControllerManager.ValueAdded -= AddPredicate;
        _addableManager.ControllerManager.ValueRemoved -= RemovePredicate;
    }

    private void AddFirstPredicates()
    {
        foreach (var keyValuePair in _addableManager.ControllerManager.Controllers)
        {
            foreach (var controller in keyValuePair.Value)
            {
                AddPredicate(keyValuePair.Key, controller);
            }
        }
    }

    private void AddPredicate(int key, TController controller)
    {
        if (_predicates.ContainsKey(key) is false)
        {
            _predicates.Add(key, new());
        }
        if (_predicates.TryGetValue(key, out var source))
        {
            var predicate = CreatePredicate(controller);
            if (source.TryAdd(controller, predicate))
            {
                controller.PredicateManager.AddValue(predicate);
            }
        }
    }

    private void RemovePredicate(int key, TController controller)
    {
        if (_predicates.TryGetValue(key, out var source))
        {
            if (source.Remove(controller, out var predicate))
            {
                controller.PredicateManager.RemoveValue(predicate);
            }
        }
    }

    protected abstract UIPredicate<TController> CreatePredicate(TController controller);

    protected abstract class UIPredicate<T> : IInputPredicate where T : IInputController
    {
        protected readonly T _controller;
        protected readonly EventSystem _eventSystem;
        protected readonly PointerEventData _eventData;
        protected readonly List<RaycastResult> _raycastResult;

        public UIPredicate(T controller, EventSystem eventSystem)
        {
            _controller = controller;
            _eventSystem = eventSystem;

            _eventData = new(eventSystem);
            _raycastResult = new();
        }

        public abstract bool Result();

        protected virtual bool IsPositionNotOverUI(in Vector2 position)
        {
            _eventData.position = position;
            _eventSystem.RaycastAll(_eventData, _raycastResult);

            return _raycastResult.Count is 0;
        }
    }
}}