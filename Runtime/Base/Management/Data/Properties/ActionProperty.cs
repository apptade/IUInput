using System;

namespace IUInput {
public class ActionProperty<T> : ValueProperty<T>, IActionProperty<T>
{
    public event Action<T> Started;
    public event Action<T> Canceled;
    public event Action<T> Performed;

    public void OnStarted(T value)
    {
        Value = value;
        Started?.Invoke(value);
    }

    public void OnCanceled(T value)
    {
        Value = value;
        Canceled?.Invoke(value);
    }

    public void OnPerformed(T value)
    {
        Value = value;
        Performed?.Invoke(value);
    }
}}