using System;
using UnityEngine;

namespace SFInput.Screen {
public sealed class ClickInputData : IInputData
{
    public Vector2 ClickDownPosition { get; private set; }
    public Vector2 ClickUpPosition { get; private set; }
    public float StaticPressTime { get; private set; }
    public int MultipleClickCount { get; private set; }
    public bool Pressed { get; private set; }

    public event Action Changed;
    public event Action<Vector2, int> ClickChanged;
    public event Action<Vector2> ClickDownChanged;
    public event Action<Vector2> ClickUpChanged;
    public event Action<float> StaticPressTimeChanged;

    internal void OnClickChanged(in Vector2 position, in int count)
    {
        MultipleClickCount = count;
        ClickChanged?.Invoke(position, count);
        Changed?.Invoke();
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

    internal void OnStaticPressTimeChanged(in float time)
    {
        StaticPressTime = time;
        StaticPressTimeChanged?.Invoke(time);
        Changed?.Invoke();
    }
}}