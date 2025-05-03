using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace IUInput.Screen {
public sealed class UIInputBreaker : MonoBehaviour
{
    [SerializeField] private InputManager<ClickInputController, ClickInputData> _clickManager;
    [SerializeField] private InputManager<MovementInputController, MovementInputData> _movementManager;
    [SerializeField] private InputManager<PinchInputController, PinchInputData> _pinchManager;

    [Space]
    [SerializeField] private EventSystem _eventSystem;

    private UIClickPredicate _clickPredicate;
    private UIMovementPredicate _movementPredicate;
    private UIPinchPredicate _pinchPredicate;

    private void Awake()
    {
        _clickPredicate = new(_eventSystem);
        _movementPredicate = new(_eventSystem);
        _pinchPredicate = new(_eventSystem);
    }

    private void OnEnable()
    {
        if (_clickManager != null) _clickManager.ControllerManager.PredicateManager.AddPredicate(_clickPredicate);
        if (_movementManager != null) _movementManager.ControllerManager.PredicateManager.AddPredicate(_movementPredicate);
        if (_pinchManager != null) _pinchManager.ControllerManager.PredicateManager.AddPredicate(_pinchPredicate);
    }

    private void OnDisable()
    {
        if (_clickManager != null) _clickManager.ControllerManager.PredicateManager.RemovePredicate(_clickPredicate);
        if (_movementManager != null) _movementManager.ControllerManager.PredicateManager.RemovePredicate(_movementPredicate);
        if (_pinchManager != null) _pinchManager.ControllerManager.PredicateManager.RemovePredicate(_pinchPredicate);
    }

    private abstract class UIPredicate<T> : IInputPredicate<T>
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

        public bool Result(T entry)
        {
            var position = GetInputPosition(entry);
            return position.HasValue is false || IsPositionNotOverUI(position.Value);
        }

        protected abstract Vector2? GetInputPosition(T entry);

        private bool IsPositionNotOverUI(in Vector2 position)
        {
            _memoryPointerEventData.position = position;
            _eventSystem.RaycastAll(_memoryPointerEventData, _memoryRaycastResult);

            return _memoryRaycastResult.Count is 0;
        }
    }

    private sealed class UIClickPredicate : UIPredicate<ClickInputController>
    {
        public UIClickPredicate(EventSystem eventSystem) : base(eventSystem){}

        protected override Vector2? GetInputPosition(ClickInputController entry)
        {
            return entry.SettableDownPosition;
        }
    }

    private sealed class UIMovementPredicate : UIPredicate<MovementInputController>
    {
        public UIMovementPredicate(EventSystem eventSystem) : base(eventSystem){}

        protected override Vector2? GetInputPosition(MovementInputController entry)
        {
            return entry.SettablePosition;
        }
    }

    private sealed class UIPinchPredicate : UIPredicate<PinchInputController>
    {
        public UIPinchPredicate(EventSystem eventSystem) : base(eventSystem){}

        protected override Vector2? GetInputPosition(PinchInputController entry)
        {
            return entry.SettableMiddlePosition;
        }
    }
}}