using System;
using UnityEngine;

namespace IUInput.Screen {
public sealed class PinchInputData : IInputData
{
    public readonly IValueProperty<float?> Pinch = new ValueProperty<float?>();
    public readonly IValueProperty<Vector2?> MiddlePosition = new ValueProperty<Vector2?>();

    public event Action<float, Vector2> PinchChanged;

    public void OnPinchChanged(in float value, in Vector2 middlePosition)
    {
        PinchChanged?.Invoke(value, middlePosition);
    }
}}