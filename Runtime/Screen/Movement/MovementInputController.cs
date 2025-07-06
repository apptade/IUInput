using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IUInput.Screen {
public sealed class MovementInputController : InputController, IDisposable
{
    private readonly InputAction _deltaInput;
    private readonly InputAction _positionInput;
    private readonly MovementInputData _movementData;

    private Vector2 _lastMovementPosition;
    private float _movementErrorNumber;

    public int MaxMovementErrorCount { get; set; }
    public Vector2? SettableDelta { get; private set; }
    public Vector2? SettablePosition { get; private set; }

    public MovementInputController(InputAction deltaInput, InputAction positionInput, MovementInputData movementData)
    {
        _deltaInput = deltaInput;
        _positionInput = positionInput;
        _movementData = movementData;
    }

    public void Dispose()
    {
        _deltaInput.Dispose();
        _positionInput.Dispose();
    }

    protected override void OnEnable()
    {
        _deltaInput.started += StartDeltaInput;
        _deltaInput.canceled += CancelDeltaInput;
        _deltaInput.Enable();

        _positionInput.started += StartPositionInput;
        _positionInput.canceled += CancelPositionInput;
        _positionInput.performed += PerformPositionInput;
        _positionInput.Enable();
    }

    protected override void OnDisable()
    {
        _deltaInput.started -= StartDeltaInput;
        _deltaInput.canceled -= CancelDeltaInput;
        _deltaInput.Disable();

        _positionInput.started -= StartPositionInput;
        _positionInput.canceled -= CancelPositionInput;
        _positionInput.performed -= PerformPositionInput;
        _positionInput.Disable();
    }

    private void StartDeltaInput(InputAction.CallbackContext context)
    {
        var delta = context.ReadValue<Vector2>();

        SettableDelta = delta;
        _movementErrorNumber += delta.magnitude;

        if (IsCanMove())
        {
            _movementData.Delta.Value = SettableDelta;
            ChangeMovement();
        }

        SettableDelta = null;
    }

    private void CancelDeltaInput(InputAction.CallbackContext context)
    {
        if (_movementData.Delta.Value.HasValue) _movementData.Delta.Value = null;
    }

    private void StartPositionInput(InputAction.CallbackContext context)
    {
        SettablePosition = context.ReadValue<Vector2>();
        if (PredicateManager.AllResult()) _movementData.Position.OnStarted(SettablePosition);
    }

    private void CancelPositionInput(InputAction.CallbackContext context)
    {
        _movementErrorNumber = 0;
        SettablePosition = null;

        if (_movementData.Position.Value.HasValue)
        {
            _movementData.Position.OnCanceled(_movementData.Position.Value);
            _movementData.Position.Value = null;
        }
    }

    private void PerformPositionInput(InputAction.CallbackContext context)
    {
        SettablePosition = context.ReadValue<Vector2>();

        if (IsCanMove())
        {
            _movementData.Position.OnPerformed(SettablePosition);
            ChangeMovement();
        }
    }

    private bool IsCanMove()
    {
        return _movementErrorNumber >= MaxMovementErrorCount && PredicateManager.AllResult();
    }

    private void ChangeMovement()
    {
        var nullableDelta = _movementData.Delta.Value;
        var nullablePosition = _movementData.Position.Value;

        if (nullableDelta.HasValue && nullablePosition.HasValue)
        {
            var delta = nullableDelta.Value;
            var position = nullablePosition.Value;

            if (_lastMovementPosition != position)
            {
                _lastMovementPosition = position;
                _movementData.OnMovementChanged(delta, position);
            }
        }
    }
}}