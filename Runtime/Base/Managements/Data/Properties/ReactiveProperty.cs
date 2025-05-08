using System;

namespace IUInput {
public sealed class ReactiveProperty<T> : IReactiveProperty<T>
{
    private T _value;

    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            Changed?.Invoke(value);
        }
    }

    public event Action<T> Changed;
}}