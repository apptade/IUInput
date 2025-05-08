using System;
using UnityEngine;

namespace IUInput.Screen {
public sealed class PinchInputData : IInputData
{
    public readonly IReactiveProperty<float?> Pinch = new ReactiveProperty<float?>();
    public readonly IReactiveProperty<Vector2?> MiddlePosition = new ReactiveProperty<Vector2?>();

    public event Action<float, Vector2> PinchChanged;

    internal void OnPinchChanged(in float value, in Vector2 middlePosition)
    {
        PinchChanged?.Invoke(value, middlePosition);
    }
}}