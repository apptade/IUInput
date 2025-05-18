using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IUInput.Screen {
public sealed class MousePinchInputController : PinchInputController, IDisposable
{
    private readonly InputAction _scrollInput;
    private bool _started;

    public MousePinchInputController(InputAction scrollInput, PinchInputData pinchData) : base(pinchData)
    {
        _scrollInput = scrollInput;
    }

    public void Dispose()
    {
        _scrollInput.Dispose();
    }

    protected override void OnEnable()
    {
        _scrollInput.started += StartScrollInput;
        _scrollInput.canceled += CancelPinchInput;
        _scrollInput.Enable();
    }

    protected override void OnDisable()
    {
        _scrollInput.started -= StartScrollInput;
        _scrollInput.canceled -= CancelPinchInput;
        _scrollInput.Disable();
    }

    private void StartScrollInput(InputAction.CallbackContext context)
    {
        SettableValue = context.ReadValue<Vector2>().y;
        SettableMiddlePosition = Mouse.current.position.value;

        if (PredicateManager.AllResult())
        {
            _started = true;
            _pinchData.Pinch.Value = SettableValue;
            _pinchData.MiddlePosition.Value = SettableMiddlePosition;
            _pinchData.OnPinchChanged(SettableValue.Value, SettableMiddlePosition.Value);
        }

        SettableValue = null;
        SettableMiddlePosition = null;
    }

    private void CancelPinchInput(InputAction.CallbackContext context)
    {
        if (_started is false) return;

        _started = false;
        _pinchData.Pinch.Value = null;
        _pinchData.MiddlePosition.Value = null;
    }
}}