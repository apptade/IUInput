using System;

namespace IUInput {
public interface IDictionaryHandler<TKey, TValue>
{
    event Action<TKey, TValue> ValueAdded;
    event Action<TKey, TValue> ValueRemoved;

    bool AddValue(TKey key, TValue value);
    bool ClearValues();
    bool RemoveKey(TKey key);
}}