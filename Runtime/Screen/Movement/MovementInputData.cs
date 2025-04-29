using System;
using UnityEngine;

namespace IUInput.Screen {
public sealed class MovementInputData : IInputData
{
    public Vector2 Delta { get; private set; }
    public Vector2 Position { get; private set; }

    public event Action Changed;
    public event Action<Vector2> DeltaChanged;
    public event Action<Vector2> PositionChanged;
    public event Action<Vector2, Vector2> MovementChanged;

    internal void OnDeltaChanged(in Vector2 delta)
    {
        Delta = delta;
        DeltaChanged?.Invoke(delta);
        Changed?.Invoke();
    }

    internal void OnPositionChanged(in Vector2 position)
    {
        Position = position;
        PositionChanged?.Invoke(position);
        Changed?.Invoke();
    }

    internal void OnMovementChanged(in Vector2 delta, in Vector2 position)
    {
        MovementChanged?.Invoke(delta, position);
    }
}}