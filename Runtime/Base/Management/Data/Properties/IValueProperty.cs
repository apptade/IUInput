using System;

namespace IUInput {
public interface IValueProperty<T>
{
    T Value { get; set; }
    event Action<T> Changed;
}}