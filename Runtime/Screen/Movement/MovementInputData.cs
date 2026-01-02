using System;
using UnityEngine;

namespace IUInput.Screen {
public sealed class MovementInputData : IInputData
{
    public readonly IValueProperty<Vector2?> Delta = new ValueProperty<Vector2?>();
    public readonly IActionProperty<Vector2?> Position = new ActionProperty<Vector2?>();

    public event Action<Vector2, Vector2> MovementChanged;

    public void OnMovementChanged(Vector2 delta, Vector2 position)
    {
        MovementChanged?.Invoke(delta, position);
    }
}}