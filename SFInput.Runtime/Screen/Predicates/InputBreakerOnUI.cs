using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace SFInput.Screen {
public sealed class InputBreakerOnUI : MonoBehaviour
{
    [SerializeField] private InputManager<MovementInputController, MovementInputData> _addableMovementManager;
    [SerializeField] private EventSystem _eventSystem;

    private IInputPredicate[] _inputPredicates;
    public int MaxPointersCount { get => 10; }

    private void Awake()
    {
        _inputPredicates = new IInputPredicate[MaxPointersCount];
        for (int i = 0; i < MaxPointersCount; i++)
        {
            _inputPredicates[i] = new UIInputPredicate(i, _eventSystem);
        }
    }

    private void OnEnable()
    {
        //_addableMovementManager.ControllerManager.AddPredicates(_inputPredicates);
    }

    private void OnDisable()
    {
        //_addableMovementManager.ControllerManager.RemovePredicates(_inputPredicates);
    }

    private sealed class UIInputPredicate : IInputPredicate
    {
        private readonly int _pointerId;
        private readonly EventSystem _eventSystem;
        private readonly PointerEventData _memoryPointerEventData;
        private readonly List<RaycastResult> _memoryRaycastResult;

        public UIInputPredicate(int pointerId, EventSystem eventSystem)
        {
            _pointerId = pointerId;
            _eventSystem = eventSystem;
            _memoryPointerEventData = new(eventSystem);
            _memoryRaycastResult = new();
        }

        public bool Result()
        {
            var mouseResult = true;
            var touchResult = true;

            if (Mouse.current != null) mouseResult = IsPositionNotOverUI(Mouse.current.position.value);
            if (Touchscreen.current != null) touchResult = IsPositionNotOverUI(Touchscreen.current.touches[_pointerId].position.value);

            return mouseResult && touchResult;
        }

        private bool IsPositionNotOverUI(in Vector2 position)
        {
            _memoryPointerEventData.position = position;
            _eventSystem.RaycastAll(_memoryPointerEventData, _memoryRaycastResult);

            return _memoryRaycastResult.Count is 0;
        }
    }
}}