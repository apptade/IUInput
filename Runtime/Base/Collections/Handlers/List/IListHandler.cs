using System;

namespace IUInput {
public interface IListHandler<TValue>
{
    event Action<TValue> ValueAdded;
    event Action<TValue> ValueRemoved;

    bool AddValue(TValue value);
    bool ClearValues();
    bool RemoveValue(TValue value);
}}