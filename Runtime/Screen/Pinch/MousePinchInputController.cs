using UnityEngine;
using UnityEngine.InputSystem;

namespace IUInput.Screen {
public sealed class MousePinchInputController : PinchInputController
{
    private readonly InputAction _pinchInput;
    private bool _pinchInputPerformed;

    public MousePinchInputController(InputAction pinchInput, PinchInputData pinchData) : base(pinchData)
    {
        _pinchInput = pinchInput;
    }

    public override void Dispose()
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
        SettableMiddlePosition = Mouse.current.position.ReadValue();

        if (PredicateManager.AllResult())
        {
            PinchData.OnPinchValueChanged(callback.ReadValue<Vector2>().y);
            PinchData.OnPinchMiddlePositionChanged(SettableMiddlePosition);
            PinchData.OnPinchChanged(PinchData.PinchValue, PinchData.PinchMiddlePosition);

            _pinchInputPerformed = true;
        }
    }

    private void CancelPinchInput(InputAction.CallbackContext callback)
    {
        if (_pinchInputPerformed)
        {
            PinchData.OnPinchValueChanged(0);
            PinchData.OnPinchMiddlePositionChanged(Vector2.zero);

            _pinchInputPerformed = false;
            SettableMiddlePosition = Vector2.zero;
        }
    }
}}