using System;
using System.Collections.Generic;

namespace IUInput {
public class DictionaryHandler<TKey, TValue> : IDictionaryHandler<TKey, TValue>
{
    protected readonly Dictionary<TKey, TValue> _source;

    public event Action<TKey, TValue> ValueAdded;
    public event Action<TKey, TValue> ValueRemoved;

    public DictionaryHandler(Dictionary<TKey, TValue> source = null)
    {
        _source = source ?? new();
    }

    public bool AddValue(TKey key, TValue value)
    {
        if (value is not null && _source.TryAdd(key, value))
        {
            ValueAdded?.Invoke(key, value);
            return true;
        }

        return false;
    }

    public bool ClearValues()
    {
        if (_source.Count is 0) return false;

        if (ValueRemoved is not null)
        {
            var memorySource = new Dictionary<TKey, TValue>(_source);
            _source.Clear();

            foreach (var keyValuePair in memorySource)
            {
                ValueRemoved.Invoke(keyValuePair.Key, keyValuePair.Value);
            }
        }
        else _source.Clear();

        return true;
    }

    public bool RemoveKey(TKey key)
    {
        if (_source.Remove(key, out var value))
        {
            ValueRemoved?.Invoke(key, value);
            return true;
        }

        return false;
    }
}}