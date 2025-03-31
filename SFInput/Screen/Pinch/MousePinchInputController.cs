using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SFInput.Screen {
public sealed class MousePinchInputController : InputController, IDisposable
{
    private readonly InputAction _scrollInput;
    private readonly PinchInputData _pinchData;

    public MousePinchInputController(PinchInputData pinchData)
    {
        _scrollInput = new(type: InputActionType.Value, binding:"<Mouse>/scroll");
        _pinchData = pinchData;
    }

    public void Dispose()
    {
        _scrollInput.Dispose();
    }

    protected override void OnEnable()
    {
        _scrollInput.performed += OnScrollInputPerformed;
        _scrollInput.Enable();
    }

    protected override void OnDisable()
    {
        _scrollInput.performed -= OnScrollInputPerformed;
        _scrollInput.Disable();
    }

    private void OnScrollInputPerformed(InputAction.CallbackContext callback)
    {
        if (PredicateManager.Result() is false) return;

        _pinchData.OnPinchChanged(callback.ReadValue<Vector2>().y, Mouse.current.position.ReadValue());
    }
}}