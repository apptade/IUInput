using System;

namespace IUInput {
public interface IReactiveProperty<T>
{
    T Value { get; set; }
    event Action<T> Changed;
}}