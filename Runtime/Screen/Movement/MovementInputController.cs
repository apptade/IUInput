using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IUInput.Screen {
public sealed class MovementInputController : InputController, IDisposable
{
    private readonly InputAction _deltaInput;
    private readonly InputAction _positionInput;
    private readonly MovementInputData _movementData;

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
        _deltaInput.canceled += CancelDeltaInput;
        _deltaInput.performed += PerformDeltaInput;
        _deltaInput.Enable();

        _positionInput.performed += PerformPositionInput;
        _positionInput.Enable();
    }

    protected override void OnDisable()
    {
        _deltaInput.canceled -= CancelDeltaInput;
        _deltaInput.performed -= PerformDeltaInput;
        _deltaInput.Disable();

        _positionInput.performed -= PerformPositionInput;
        _positionInput.Disable();
    }

    private void PerformDeltaInput(InputAction.CallbackContext callback)
    {
        SettableDelta = callback.ReadValue<Vector2>();

        if (PredicateManager.AllResult())
        {
            _movementData.OnDeltaChanged(SettableDelta.Value);
        }

        SettableDelta = null;
    }

    private void CancelDeltaInput(InputAction.CallbackContext callback)
    {
        if (_movementData.Delta != Vector2.zero)
        {
            _movementData.OnMovementChanged(_movementData.Delta, _movementData.Position);
            _movementData.OnDeltaChanged(Vector2.zero);
        }
    }

    private void PerformPositionInput(InputAction.CallbackContext callback)
    {
        SettablePosition = callback.ReadValue<Vector2>();

        if (PredicateManager.AllResult())
        {
            _movementData.OnPositionChanged(SettablePosition.Value);
        }

        SettablePosition = null;
    }
}}