using System;
using System.Collections.Generic;

namespace IUInput {
public class DoubleDictionaryHandler<TKey, TValue> : IDoubleDictionaryHandler<TKey, TValue>
{
    protected readonly Dictionary<TKey, IReadOnlyList<TValue>> _source;

    public event Action<TKey, TValue> ValueAdded;
    public event Action<TKey, TValue> ValueRemoved;

    public DoubleDictionaryHandler(Dictionary<TKey, IReadOnlyList<TValue>> source = null)
    {
        _source = source ?? new();
    }

    public bool AddValue(TKey key, TValue value)
    {
        if (value is null) return false;

        if (_source.TryGetValue(key, out var values))
        {
            ((IList<TValue>)values).Add(value);
            ValueAdded?.Invoke(key, value);
        }
        else
        {
            _source.Add(key, new List<TValue>{ value });
            ValueAdded?.Invoke(key, value);
        }

        return true;
    }

    public bool ClearValues()
    {
        if (_source.Count is 0) return false;

        if (ValueRemoved is not null)
        {
            var memorySource = new Dictionary<TKey, IReadOnlyList<TValue>>(_source);
            _source.Clear();

            foreach (var keyValuePair in memorySource)
            {
                foreach (var value in keyValuePair.Value)
                {
                    ValueRemoved.Invoke(keyValuePair.Key, value);
                }
            }
        }
        else _source.Clear();

        return true;
    }

    public bool RemoveKey(TKey key)
    {
        if (_source.Remove(key, out var collection))
        {
            if (ValueRemoved is not null)
            {
                foreach (var value in collection) ValueRemoved.Invoke(key, value);
            }

            return true;
        }

        return false;
    }

    public bool RemoveValue(TKey key, TValue value)
    {
        if (_source.TryGetValue(key, out var collection))
        {
            if (((IList<TValue>)collection).Remove(value))
            {
                ValueRemoved?.Invoke(key, value);
                return true;
            }
        }

        return false;
    }
}}