using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace IUInput.Screen {
public abstract class UIInputBreaker<TController, TData> : MonoBehaviour where TController : IInputController where TData : IInputData
{
    [SerializeField] private InputAdder<TController, TData> _inputAdder;
    [SerializeField] protected EventSystem _eventSystem;

    private Dictionary<int, UIPredicate<TController>> _predicates;

    private void Awake()
    {
        _predicates = new(_inputAdder.Controllers.Count);

        foreach (var controller in _inputAdder.Controllers)
        {
            _predicates.Add(controller.Key, CreatePredicate(controller.Value));
        }
    }

    private void OnEnable()
    {
        foreach (var predicate in _predicates)
        {
            _inputAdder.Controllers[predicate.Key].PredicateManager.AddPredicate(predicate.Value);
        }
    }

    private void OnDisable()
    {
        foreach (var predicate in _predicates)
        {
            _inputAdder.Controllers[predicate.Key].PredicateManager.RemovePredicate(predicate.Value);
        }
    }

    protected abstract UIPredicate<TController> CreatePredicate(TController controller);

    protected abstract class UIPredicate<T> : IPredicate where T : IInputController
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