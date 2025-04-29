using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace IUInput.Screen {
public sealed class InputBreakerOnUI : MonoBehaviour
{
    [SerializeField] private InputManager<ClickInputController, ClickInputData> _clickManager;
    [SerializeField] private InputManager<MovementInputController, MovementInputData> _movementManager;
    [SerializeField] private InputManager<PinchInputController, PinchInputData> _pinchManager;
    [SerializeField] private EventSystem _eventSystem;

    private UIClickInputPredicate _clickInputPredicate;
    private UIMovementInputPredicate _movementInputPredicate;
    private UIPinchInputPredicate _pinchInputPredicate;

    private void Awake()
    {
        _clickInputPredicate = new(_eventSystem);
        _movementInputPredicate = new(_eventSystem);
        _pinchInputPredicate = new(_eventSystem);
    }

    private void OnEnable()
    {
        _clickManager.ControllerManager.PredicateManager.AddPredicate(_clickInputPredicate);
        _movementManager.ControllerManager.PredicateManager.AddPredicate(_movementInputPredicate);
        _pinchManager.ControllerManager.PredicateManager.AddPredicate(_pinchInputPredicate);
    }

    private void OnDisable()
    {
        _clickManager.ControllerManager.PredicateManager.RemovePredicate(_clickInputPredicate);
        _movementManager.ControllerManager.PredicateManager.RemovePredicate(_movementInputPredicate);
        _pinchManager.ControllerManager.PredicateManager.RemovePredicate(_pinchInputPredicate);
    }

    private abstract class UIInputPredicate<T> : IInputPredicate<T>
    {
        private readonly EventSystem _eventSystem;
        private readonly PointerEventData _memoryPointerEventData;
        private readonly List<RaycastResult> _memoryRaycastResult;

        public UIInputPredicate(EventSystem eventSystem)
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

    private sealed class UIClickInputPredicate : UIInputPredicate<ClickInputController>
    {
        public UIClickInputPredicate(EventSystem eventSystem) : base(eventSystem){}

        public override bool Result(ClickInputController entry)
        {
            return IsPositionNotOverUI(entry.SettableClickDownPosition);
        }
    }

    private sealed class UIMovementInputPredicate : UIInputPredicate<MovementInputController>
    {
        public UIMovementInputPredicate(EventSystem eventSystem) : base(eventSystem){}

        public override bool Result(MovementInputController entry)
        {
            return IsPositionNotOverUI(entry.SettablePosition);
        }
    }

    private sealed class UIPinchInputPredicate : UIInputPredicate<PinchInputController>
    {
        public UIPinchInputPredicate(EventSystem eventSystem) : base(eventSystem){}

        public override bool Result(PinchInputController entry)
        {
            return IsPositionNotOverUI(entry.SettableMiddlePosition);
        }
    }
}}