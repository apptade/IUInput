using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IUInput.Screen {
public abstract class ClickInputController : InputController, IDisposable
{
    private readonly InputAction _clickInput;
    private readonly MovementInputData _movementData;
    private readonly ClickInputData _clickData;

    private bool _pressedState;
    private float _multipleClickTimer;
    private int _multipleClickCount;

    public abstract Vector2 PointerPosition { get; }

    public float MaxMultipleClickDuration { get; set; }
    public Vector2? SettableStartPosition { get; private set; }

    public ClickInputController(InputAction clickInput, MovementInputData movementData, ClickInputData clickData)
    {
        _clickInput = clickInput;
        _movementData = movementData;
        _clickData = clickData;
    }

    public void Dispose()
    {
        _clickInput.Dispose();
    }

    public void FixedUpdate()
    {
        UpdateClickHoldTime();
        UpdateMultipleClickCount();
    }

    protected override void OnEnable()
    {
        _movementData.Delta.Changed += ChangeDelta;
        _movementData.Position.Changed += ChangePosition;
        _movementData.MovementChanged += ChangeMovement;

        _clickInput.started += StartClickInput;
        _clickInput.canceled += CancelClickInput;
        _clickInput.Enable();
    }

    protected override void OnDisable()
    {
        _movementData.Delta.Changed -= ChangeDelta;
        _movementData.Position.Changed -= ChangePosition;
        _movementData.MovementChanged -= ChangeMovement;

        _clickInput.started -= StartClickInput;
        _clickInput.canceled -= CancelClickInput;
        _clickInput.Disable();
    }

    private void ChangeDelta(Vector2? delta)
    {
        if (_pressedState) _clickData.Delta.Value = delta;
    }

    private void ChangePosition(Vector2? position)
    {
        if (_pressedState) _clickData.Position.Value = position;
    }

    private void ChangeMovement(Vector2 delta, Vector2 position)
    {
        if (_pressedState) _clickData.OnClickMovementChanged(delta, position);
    }

    private void StartClickInput(InputAction.CallbackContext callback)
    {
        SettableStartPosition = PointerPosition;

        if (PredicateManager.AllResult())
        {
            _pressedState = true;

            _clickData.Pressed.Value = true;
            _clickData.Position.Value = SettableStartPosition;
            _clickData.StartPosition.Value = SettableStartPosition;
        }

        SettableStartPosition = null;
    }

    private void CancelClickInput(InputAction.CallbackContext callback)
    {
        if (_pressedState is false) return;

        ChangeStaticClick();
        _clickData.CancelPosition.Value = _movementData.Position.Value;

        _pressedState = false;

        _clickData.Pressed.Value = false;
        _clickData.StartPosition.Value = null;
        _clickData.CancelPosition.Value = null;
        _clickData.Position.Value = null;
        _clickData.Delta.Value = null;
        _clickData.HoldTime.Value = null;
    }

    private void UpdateMultipleClickCount()
    {
        if (_multipleClickCount is 0) return;

        _multipleClickTimer += Time.fixedUnscaledDeltaTime;
        if (_multipleClickTimer > MaxMultipleClickDuration)
        {
            _multipleClickCount = 0;
            _multipleClickTimer = 0;
        }
    }

    private void ChangeStaticClick()
    {
        if (IsMovementNotChanged() is false) return;

        _multipleClickTimer = 0;
        _clickData.OnStaticClickChanged(_clickData.Position.Value.Value, _multipleClickCount++);
    }

    private void UpdateClickHoldTime()
    {
        if (IsMovementNotChanged())
        {
            var time = _clickData.HoldTime.Value.GetValueOrDefault() + Time.fixedUnscaledDeltaTime;
            _clickData.HoldTime.Value = time;
        }
        else if (_clickData.HoldTime.Value is not null)
        {
            _clickData.HoldTime.Value = null;
        }
    }

    private bool IsMovementNotChanged()
    {
        if (_movementData.Position.Value.HasValue is false) return false;
        return _pressedState && Vector2.Distance(_clickData.StartPosition.Value.Value, _movementData.Position.Value.Value) < 0.1f;
    }
}}