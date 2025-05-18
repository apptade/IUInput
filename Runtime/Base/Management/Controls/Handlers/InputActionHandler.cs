using System;
using UnityEngine.InputSystem;

namespace IUInput {
public abstract class InputActionHandler : IDisposable
{
    private bool _enabled;
    public readonly InputAction Action;

    public InputActionHandler(InputAction action)
    {
        Action = action;
    }

    public virtual void Dispose()
    {
        Action.Dispose();
    }

    public void Enable()
    {
        if (_enabled) return;
        else _enabled = true;

        Action.started += StartInput;
        Action.canceled += CancelInput;
        Action.performed += PerformInput;
        Action.Enable();
    }

    public void Disable()
    {
        if (!_enabled) return;
        else _enabled = false;

        Action.started -= StartInput;
        Action.canceled -= CancelInput;
        Action.performed -= PerformInput;
        Action.Disable();
    }

    protected abstract void StartInput(InputAction.CallbackContext context);
    protected abstract void CancelInput(InputAction.CallbackContext context);
    protected abstract void PerformInput(InputAction.CallbackContext context);
}}