using UnityEngine;

namespace IUInput.Screen {
public abstract class PinchInputController : InputController
{
    protected readonly PinchInputData _pinchData;

    public float? SettableValue { get; protected set; }
    public Vector2? SettableMiddlePosition { get; protected set; }

    public PinchInputController(PinchInputData pinchData)
    {
        _pinchData = pinchData;
    }
}}