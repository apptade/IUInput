using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IUInput.Screen {
public sealed class MovementInputController : InputController, IDisposable
{
    private readonly InputAction _deltaInput;
    private readonly InputAction _positionInput;
    public readonly MovementInputData MovementData;

    public Vector2? SettableDelta { get; private set; }
    public Vector2? SettablePosition { get; private set; }

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
        _deltaInput.performed += PerformDeltaInput;
        _deltaInput.Enable();

        _positionInput.canceled += CancelPositionInput;
        _positionInput.performed += PerformPositionInput;
        _positionInput.Enable();
    }

    protected override void OnDisable()
    {
        _deltaInput.canceled -= CancelDeltaInput;
        _deltaInput.performed -= PerformDeltaInput;
        _deltaInput.Disable();

        _positionInput.canceled -= CancelPositionInput;
        _positionInput.performed -= PerformPositionInput;
        _positionInput.Disable();
    }

    private void PerformDeltaInput(InputAction.CallbackContext callback)
    {
        SettableDelta = callback.ReadValue<Vector2>();

        if (PredicateManager.AllResult())
        {
            MovementData.Delta.Value = SettableDelta;
        }

        SettableDelta = null;
    }

    private void CancelDeltaInput(InputAction.CallbackContext callback)
    {
        if (MovementData.Delta.Value.HasValue)
        {
            MovementData.OnMovementChanged(MovementData.Delta.Value.Value, MovementData.Position.Value.Value);
            MovementData.Delta.Value = null;
        }
    }

    private void PerformPositionInput(InputAction.CallbackContext callback)
    {
        SettablePosition = callback.ReadValue<Vector2>();

        if (PredicateManager.AllResult())
        {
            MovementData.Position.Value = SettablePosition;
        }

        SettablePosition = null;
    }

    private void CancelPositionInput(InputAction.CallbackContext callback)
    {
        if (MovementData.Position.Value.HasValue)
        {
            MovementData.Position.Value = null;
        }
    }
}}