using System;
using UnityEngine;

namespace SFInput.Screen {
public sealed class ClickInputData : IInputData
{
    public bool Pressed { get; private set; }
    public Vector2 ClickDelta { get; private set; }
    public Vector2 ClickPosition { get; private set; }
    public Vector2 ClickDownPosition { get; private set; }
    public Vector2 ClickUpPosition { get; private set; }
    public float StaticHoldTime { get; private set; }

    public event Action Changed;
    public event Action<Vector2> ClickDeltaChanged;
    public event Action<Vector2> ClickPositionChanged;
    public event Action<Vector2, Vector2> ClickMovementChanged;
    public event Action<Vector2> ClickDownChanged;
    public event Action<Vector2> ClickUpChanged;
    public event Action<Vector2, int> StaticClickChanged;
    public event Action<float> StaticHoldTimeChanged;

    internal void OnClickDeltaChanged(in Vector2 delta)
    {
        ClickDelta = delta;
        ClickDeltaChanged?.Invoke(delta);
        Changed?.Invoke();
    }

    internal void OnClickPositionChanged(in Vector2 position)
    {
        ClickPosition = position;
        ClickPositionChanged?.Invoke(position);
        Changed?.Invoke();
    }

    internal void OnClickMovementChanged(in Vector2 delta, in Vector2 position)
    {
        ClickMovementChanged?.Invoke(delta, position);
    }

    internal void OnClickDownChanged(in Vector2 position)
    {
        Pressed = true;
        ClickDownPosition = position;
        ClickDownChanged?.Invoke(position);
        Changed?.Invoke();
    }

    internal void OnClickUpChanged(in Vector2 position)
    {
        Pressed = false;
        ClickUpPosition = position;
        ClickUpChanged?.Invoke(position);
        Changed?.Invoke();
    }

    internal void OnStaticClickChanged(in Vector2 position, int count)
    {
        StaticClickChanged?.Invoke(position, count);
    }

    internal void OnStaticHoldTimeChanged(in float time)
    {
        StaticHoldTime = time;
        StaticHoldTimeChanged?.Invoke(time);
        Changed?.Invoke();
    }
}}