using System;
using System.Collections.Generic;

namespace IUInput {
public class ReactiveDictionary<TKey, TItem> : IReactiveDictionary<TKey, TItem>
{
    protected readonly Dictionary<TKey, TItem> _items;

    public event Action<TKey, TItem> ItemAdded;
    public event Action<TKey, TItem> ItemRemoved;

    public ReactiveDictionary(Dictionary<TKey, TItem> items = null)
    {
        _items = items ?? new();
    }

    public bool Add(TKey key, TItem item)
    {
        if (item is not null && _items.TryAdd(key, item))
        {
            ItemAdded?.Invoke(key, item);
            return true;
        }

        return false;
    }

    public bool Clear()
    {
        if (_items.Count is 0) return false;

        if (ItemRemoved is not null)
        {
            var memorySource = new Dictionary<TKey, TItem>(_items);
            _items.Clear();

            foreach (var item in memorySource)
            {
                ItemRemoved.Invoke(item.Key, item.Value);
            }
        }
        else _items.Clear();

        return true;
    }

    public bool Remove(TKey key)
    {
        if (_items.Remove(key, out var value))
        {
            ItemRemoved?.Invoke(key, value);
            return true;
        }

        return false;
    }

    public void ForEach(Action<TKey, TItem> action)
    {
        foreach (var item in _items) action(item.Key, item.Value);
    }

    public void ForEach(Func<TKey, TItem, bool> breaker)
    {
        foreach (var item in _items)
        {
            if (breaker(item.Key, item.Value)) break;
        }
    }
}}