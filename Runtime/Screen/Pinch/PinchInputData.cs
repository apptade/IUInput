using System;
using UnityEngine;

namespace AFUInput.Screen {
public sealed class PinchInputData : IInputData
{
    public float PinchValue { get; private set; }
    public Vector2 PinchMiddlePosition { get; private set; }

    public event Action Changed;
    public event Action<float> PinchValueChanged;
    public event Action<Vector2> PinchMiddlePositionChanged;
    public event Action<float, Vector2> PinchChanged;

    internal void OnPinchValueChanged(in float value)
    {
        PinchValue = value;
        PinchValueChanged?.Invoke(value);
        Changed?.Invoke();
    }

    internal void OnPinchMiddlePositionChanged(in Vector2 middlePosition)
    {
        PinchMiddlePosition = middlePosition;
        PinchMiddlePositionChanged?.Invoke(middlePosition);
        Changed?.Invoke();
    }

    internal void OnPinchChanged(in float value, in Vector2 middlePosition)
    {
        PinchChanged?.Invoke(value, middlePosition);
    }
}}