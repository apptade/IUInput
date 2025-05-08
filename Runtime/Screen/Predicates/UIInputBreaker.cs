using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace IUInput.Screen {
public abstract class UIInputBreaker<TController, TData> : MonoBehaviour where TController : IInputController where TData : IInputData
{
    [SerializeField] private InputManager<TController, TData> _addableManager;
    [SerializeField] protected EventSystem _eventSystem;

    private UIPredicate<TController> _uIPredicate;

    private void Awake()
    {
        _uIPredicate = InitializePredicate();
    }

    private void OnEnable()
    {
        _addableManager.ControllerManager.PredicateManager.AddPredicate(_uIPredicate);
    }

    private void OnDisable()
    {
        _addableManager.ControllerManager.PredicateManager.RemovePredicate(_uIPredicate);
    }

    protected abstract UIPredicate<TController> InitializePredicate();

    protected abstract class UIPredicate<T> : IInputPredicate<T>
    {
        private readonly EventSystem _eventSystem;
        private readonly PointerEventData _memoryPointerEventData;
        private readonly List<RaycastResult> _memoryRaycastResult;

        public UIPredicate(EventSystem eventSystem)
        {
            _eventSystem = eventSystem;
            _memoryPointerEventData = new(eventSystem);
            _memoryRaycastResult = new();
        }

        public abstract bool Result(T entry);

        protected bool IsPositionNotOverUI(in Vector2 position)
        {
            _memoryPointerEventData.position = position;
            _eventSystem.RaycastAll(_memoryPointerEventData, _memoryRaycastResult);

            return _memoryRaycastResult.Count is 0;
        }
    }
}}