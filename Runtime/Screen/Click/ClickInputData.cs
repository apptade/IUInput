using System;
using UnityEngine;

namespace IUInput.Screen {
public sealed class ClickInputData : IInputData
{
    public readonly IReactiveProperty<bool> Pressed = new ReactiveProperty<bool>();
    public readonly IReactiveProperty<Vector2?> StartPosition = new ReactiveProperty<Vector2?>();
    public readonly IReactiveProperty<Vector2?> CancelPosition = new ReactiveProperty<Vector2?>();
    public readonly IReactiveProperty<Vector2?> Position = new ReactiveProperty<Vector2?>();
    public readonly IReactiveProperty<Vector2?> Delta = new ReactiveProperty<Vector2?>();
    public readonly IReactiveProperty<float?> HoldTime = new ReactiveProperty<float?>();

    public event Action<Vector2, Vector2> ClickMovementChanged;
    public event Action<Vector2, int> StaticClickChanged;

    internal void OnClickMovementChanged(in Vector2 delta, in Vector2 position)
    {
        ClickMovementChanged?.Invoke(delta, position);
    }

    internal void OnStaticClickChanged(in Vector2 position, int count)
    {
        StaticClickChanged?.Invoke(position, count);
    }
}}