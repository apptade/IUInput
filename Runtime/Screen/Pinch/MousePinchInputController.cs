using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IUInput.Screen {
public sealed class MousePinchInputController : PinchInputController, IDisposable
{
    private readonly InputAction _pinchInput;

    public MousePinchInputController(InputAction pinchInput, PinchInputData pinchData) : base(pinchData)
    {
        _pinchInput = pinchInput;
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
        SettableValue = callback.ReadValue<Vector2>().y;
        SettableMiddlePosition = Mouse.current.position.ReadValue();

        if (PredicateManager.AllResult())
        {
            _pinchData.OnPinchValueChanged(SettableValue.Value);
            _pinchData.OnPinchMiddlePositionChanged(SettableMiddlePosition.Value);
            _pinchData.OnPinchChanged(SettableValue.Value, SettableMiddlePosition.Value);
        }

        SettableValue = null;
        SettableMiddlePosition = null;
    }

    private void CancelPinchInput(InputAction.CallbackContext callback)
    {
        if (_pinchData.PinchValue is not 0)
        {
            _pinchData.OnPinchValueChanged(0);
            _pinchData.OnPinchMiddlePositionChanged(Vector2.zero);
        }
    }
}}