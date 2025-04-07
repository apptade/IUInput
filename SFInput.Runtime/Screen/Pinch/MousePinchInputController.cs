using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SFInput.Screen {
public sealed class MousePinchInputController : InputController, IDisposable
{
    private readonly InputAction _pinchInput;
    private readonly PinchInputData _pinchData;

    private bool _pinchInputPerformed;

    public MousePinchInputController(InputAction pinchInput, PinchInputData pinchData)
    {
        _pinchInput = pinchInput;
        _pinchData = pinchData;
    }

    public void Dispose()
    {
        _pinchInput.Dispose();
    }

    protected override void OnEnable()
    {
        _pinchInput.performed += PerformPinchInput;
        _pinchInput.canceled += CancelPinchInput;
        _pinchInput.Enable();
    }

    protected override void OnDisable()
    {
        _pinchInput.performed -= PerformPinchInput;
        _pinchInput.canceled -= CancelPinchInput;
        _pinchInput.Disable();
    }

    private void PerformPinchInput(InputAction.CallbackContext callback)
    {
        if (PredicateManager.Result())
        {
            _pinchData.OnPinchValueChanged(callback.ReadValue<Vector2>().y);
            _pinchData.OnPinchMiddlePositionChanged(Mouse.current.position.ReadValue());
            _pinchData.OnPinchChanged(_pinchData.PinchValue, _pinchData.PinchMiddlePosition);

            _pinchInputPerformed = true;
        }
    }

    private void CancelPinchInput(InputAction.CallbackContext callback)
    {
        if (_pinchInputPerformed)
        {
            _pinchData.OnPinchValueChanged(0);
            _pinchData.OnPinchMiddlePositionChanged(Vector2.zero);

            _pinchInputPerformed = false;
        }
    }
}}