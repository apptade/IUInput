using System;

namespace IUInput {
public interface IActionProperty<T> : IValueProperty<T>
{
    event Action<T> Started;
    event Action<T> Canceled;
    event Action<T> Performed;

    void OnStarted(T value);
    void OnCanceled(T value);
    void OnPerformed(T value);
}}