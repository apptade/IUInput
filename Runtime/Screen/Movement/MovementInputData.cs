using System;
using UnityEngine;

namespace IUInput.Screen {
public sealed class MovementInputData : IInputData
{
    public readonly IReactiveProperty<Vector2?> Delta = new ReactiveProperty<Vector2?>();
    public readonly IReactiveProperty<Vector2?> Position = new ReactiveProperty<Vector2?>();

    public event Action<Vector2, Vector2> MovementChanged;

    internal void OnMovementChanged(in Vector2 delta, in Vector2 position)
    {
        MovementChanged?.Invoke(delta, position);
    }
}}