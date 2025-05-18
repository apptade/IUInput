using System;
using System.Collections.Generic;

namespace IUInput {
public class ListHandler<TValue> : IListHandler<TValue>
{
    protected readonly List<TValue> _source;

    public event Action<TValue> ValueAdded;
    public event Action<TValue> ValueRemoved;

    public ListHandler(List<TValue> source = null)
    {
        _source = source ?? new();
    }

    public bool AddValue(TValue value)
    {
        if (value is null) return false;

        _source.Add(value);
        ValueAdded?.Invoke(value);

        return true;
    }

    public bool ClearValues()
    {
        if (_source.Count is 0) return false;

        if (ValueRemoved is not null)
        {
            var memorySource = new List<TValue>(_source);
            _source.Clear();

            foreach (var value in memorySource)
            {
                ValueRemoved.Invoke(value);
            }
        }
        else _source.Clear();

        return true;
    }

    public bool RemoveValue(TValue value)
    {
        if (_source.Remove(value))
        {
            ValueRemoved?.Invoke(value);
            return true;
        }

        return false;
    }
}}