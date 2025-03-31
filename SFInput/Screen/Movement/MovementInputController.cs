using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SFInput.Screen {
public sealed class MovementInputController : InputController, IDisposable
{
    private readonly InputAction _deltaInput;
    private readonly InputAction _positionInput;
    private readonly MovementInputData _movementData;

    private bool _deltaInputExecuted;
    private bool _positionInputExecuted;

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
        _deltaInput.started += OnDeltaInputPerformed;
        _deltaInput.performed += OnDeltaInputPerformed;
        _deltaInput.canceled += OnDeltaInputCanceled;
        _deltaInput.Enable();

        _positionInput.started += OnPositionInputPerformed;
        _positionInput.performed += OnPositionInputPerformed;
        _positionInput.canceled += OnPositionInputCanceled;
        _positionInput.Enable();
    }

    protected override void OnDisable()
    {
        _deltaInput.started -= OnDeltaInputPerformed;
        _deltaInput.performed -= OnDeltaInputPerformed;
        _deltaInput.canceled -= OnDeltaInputCanceled;
        _deltaInput.Disable();

        _positionInput.started -= OnPositionInputPerformed;
        _positionInput.performed -= OnPositionInputPerformed;
        _positionInput.canceled -= OnPositionInputCanceled;
        _positionInput.Disable();
    }

    private void OnDeltaInputPerformed(InputAction.CallbackContext callback)
    {
        if (PredicateManager.Result() is false) return;

        _deltaInputExecuted = true;
        _movementData.OnDeltaChanged(callback.ReadValue<Vector2>());
        TryChangeMovement();
    }

    private void OnDeltaInputCanceled(InputAction.CallbackContext callback)
    {
        _deltaInputExecuted = false;
        _movementData.OnDeltaChanged(Vector2.zero);
    }

    private void OnPositionInputPerformed(InputAction.CallbackContext callback)
    {
        if (PredicateManager.Result() is false) return;

        _positionInputExecuted = true;
        _movementData.OnPositionChanged(callback.ReadValue<Vector2>());
        TryChangeMovement();
    }

    private void OnPositionInputCanceled(InputAction.CallbackContext callback)
    {
        _positionInputExecuted = false;
    }

    private void TryChangeMovement()
    {
        if (_deltaInputExecuted && _positionInputExecuted)
        {
            _movementData.OnMovementChanged(_movementData.Delta, _movementData.Position);
        }
    }
}}