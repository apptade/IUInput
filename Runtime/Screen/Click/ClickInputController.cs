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
    private float _errorMovementCount;
    private float _multipleClickTimer;
    private int _multipleClickCount;

    public bool BlockErrorMovement { get; set; }
    public float MaxErrorMovementCount { get; set; }
    public float MaxMultipleClickDuration { get; set; }
    public Vector2? SettableDownPosition { get; private set; }

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
        UpdateMultipleClickCount();
        UpdateClickStaticHoldTime();
    }

    protected override void OnEnable()
    {
        _movementData.DeltaChanged += ChangeClickDelta;
        _movementData.PositionChanged += ChangeClickPosition;
        _movementData.MovementChanged += ChangeClickMovement;

        _clickInput.started += StartClickInput;
        _clickInput.canceled += CancelClickInput;
        _clickInput.Enable();
    }

    protected override void OnDisable()
    {
        _movementData.DeltaChanged -= ChangeClickDelta;
        _movementData.PositionChanged -= ChangeClickPosition;
        _movementData.MovementChanged -= ChangeClickMovement;

        _clickInput.started -= StartClickInput;
        _clickInput.canceled -= CancelClickInput;
        _clickInput.Disable();
    }

    protected abstract Vector2 CurrentPointerPosition();

    private void ChangeClickDelta(Vector2 delta)
    {
        if (_pressedState)
        {
            _errorMovementCount += delta.magnitude;
            if (CanChangeMovement()) _clickData.OnClickDeltaChanged(delta);
        }
    }

    private void ChangeClickPosition(Vector2 position)
    {
        if (_pressedState && CanChangeMovement())
        {
            _clickData.OnClickPositionChanged(position);
        }
    }

    private void ChangeClickMovement(Vector2 delta, Vector2 position)
    {
        if (_pressedState && CanChangeMovement())
        {
            _clickData.OnClickMovementChanged(delta, position);
        }
    }

    private void StartClickInput(InputAction.CallbackContext callback)
    {
        SettableDownPosition = CurrentPointerPosition();

        if (PredicateManager.AllResult())
        {
            _pressedState = true;

            _clickData.OnClickDownChanged(SettableDownPosition.Value);
            _clickData.OnClickPositionChanged(SettableDownPosition.Value);
        }

        SettableDownPosition = null;
    }

    private void CancelClickInput(InputAction.CallbackContext callback)
    {
        if (_pressedState is false) return;
        else _clickData.OnClickUpChanged(_movementData.Position);

        if (IsStaticClick())
        {
            _multipleClickTimer = 0;
            _clickData.OnStaticClickChanged(_movementData.Position, _multipleClickCount++);
        }

        _pressedState = false;
        _errorMovementCount = 0;

        _clickData.OnStaticHoldTimeChanged(0);
        _clickData.OnClickDeltaChanged(Vector2.zero);
        _clickData.OnClickPositionChanged(Vector2.zero);
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

    private void UpdateClickStaticHoldTime()
    {
        if (_pressedState is false) return;

        if (IsStaticClick())
        {
            var time = _clickData.StaticHoldTime + Time.fixedUnscaledDeltaTime;
            _clickData.OnStaticHoldTimeChanged(time);
        }
        else if (_clickData.StaticHoldTime is not 0)
        {
            _clickData.OnStaticHoldTimeChanged(0);
        }
    }

    private bool CanChangeMovement()
    {
        if (BlockErrorMovement is false) return true;
        else return _errorMovementCount >= MaxErrorMovementCount;
    }

    private bool IsStaticClick()
    {
        return _errorMovementCount <= MaxErrorMovementCount;
    }
}}