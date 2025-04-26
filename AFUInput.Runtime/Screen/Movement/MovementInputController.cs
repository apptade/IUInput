using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AFUInput.Screen {
public sealed class MovementInputController : InputController, IDisposable
{
    private readonly InputAction _deltaInput;
    private readonly InputAction _positionInput;
    public readonly MovementInputData MovementData;

    public Vector2 SettableDelta { get; private set; }
    public Vector2 SettablePosition { get; private set; }
    public int PositionInputExecuteCount { get; private set; }

    public MovementInputController(InputAction deltaInput, InputAction positionInput, MovementInputData movementData)
    {
        _deltaInput = deltaInput;
        _positionInput = positionInput;
        MovementData = movementData;
    }

    public void Dispose()
    {
        _deltaInput.Dispose();
        _positionInput.Dispose();
    }

    protected override void OnEnable()
    {
        _deltaInput.canceled += CancelDeltaInput;
        _deltaInput.Enable();

        _positionInput.started += StartPositionInput;
        _positionInput.canceled += CancelPositionInput;
        _positionInput.performed += PerformPositionInput;
        _positionInput.Enable();
    }

    protected override void OnDisable()
    {
        _deltaInput.canceled -= CancelDeltaInput;
        _deltaInput.Disable();

        _positionInput.started -= StartPositionInput;
        _positionInput.canceled -= CancelPositionInput;
        _positionInput.performed -= PerformPositionInput;
        _positionInput.Disable();
    }

    private void CancelDeltaInput(InputAction.CallbackContext callback)
    {
        SettableDelta = Vector2.zero;

        if (MovementData.Delta != Vector2.zero)
        {
            MovementData.OnDeltaChanged(Vector2.zero);
        }
    }

    private void StartPositionInput(InputAction.CallbackContext callback)
    {
        SettablePosition = callback.ReadValue<Vector2>();

        if (PredicateManager.AllResult())
        {
            MovementData.OnPositionChanged(SettablePosition);
        }

        PositionInputExecuteCount = 1;
    }

    private void CancelPositionInput(InputAction.CallbackContext callback)
    {
        SettablePosition = Vector2.zero;

        if (MovementData.Position != Vector2.zero)
        {
            MovementData.OnPositionChanged(Vector2.zero);
        }
    }

    private void PerformPositionInput(InputAction.CallbackContext callback)
    {
        if (PositionInputExecuteCount > 1)
        {
            SettableDelta = _deltaInput.ReadValue<Vector2>();
            SettablePosition = _positionInput.ReadValue<Vector2>();

            if (PredicateManager.AllResult())
            {
                MovementData.OnDeltaChanged(SettableDelta);
                MovementData.OnPositionChanged(SettablePosition);
                MovementData.OnMovementChanged(SettableDelta, SettablePosition);
            }
        }

        PositionInputExecuteCount++;
    }
}}