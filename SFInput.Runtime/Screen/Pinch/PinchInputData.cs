using System;
using UnityEngine;

namespace SFInput.Screen {
public sealed class PinchInputData : IInputData
{
    public float PinchValue { get; private set; }
    public Vector2 MiddlePinchPosition { get; private set; }

    public event Action Changed;
    public event Action<float, Vector2> PinchChanged;

    internal void OnPinchChanged(in float value, in Vector2 middlePosition)
    {
        PinchValue = value;
        MiddlePinchPosition = middlePosition;

        PinchChanged?.Invoke(value, middlePosition);
        Changed?.Invoke();
    }
}}