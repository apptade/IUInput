using System;
using System.Collections.Generic;

namespace IUInput {
public class ReactiveDoubleDictionary<TKey, TItem> : IReactiveDoubleDictionary<TKey, TItem>
{
    protected readonly Dictionary<TKey, IReadOnlyList<TItem>> _items;

    public event Action<TKey, TItem> ItemAdded;
    public event Action<TKey, TItem> ItemRemoved;

    public ReactiveDoubleDictionary(Dictionary<TKey, IReadOnlyList<TItem>> items = null)
    {
        _items = items ?? new();
    }

    public bool Add(TKey key, TItem item)
    {
        if (item is null) return false;

        if (_items.TryGetValue(key, out var values))
        {
            ((IList<TItem>)values).Add(item);
        }
        else
        {
            _items.Add(key, new List<TItem>{ item });
        }

        ItemAdded?.Invoke(key, item);
        return true;
    }

    public bool Clear()
    {
        if (_items.Count is 0) return false;

        if (ItemRemoved is not null)
        {
            var memorySource = new Dictionary<TKey, IReadOnlyList<TItem>>(_items);
            _items.Clear();

            foreach (var item in memorySource)
            {
                foreach (var value in item.Value)
                {
                    ItemRemoved.Invoke(item.Key, value);
                }
            }
        }
        else _items.Clear();

        return true;
    }

    public bool Remove(TKey key)
    {
        if (_items.Remove(key, out var collection))
        {
            if (ItemRemoved is not null)
            {
                foreach (var value in collection) ItemRemoved.Invoke(key, value);
            }

            return true;
        }

        return false;
    }

    public bool Remove(TKey key, TItem item)
    {
        if (_items.TryGetValue(key, out var collection))
        {
            if (((IList<TItem>)collection).Remove(item))
            {
                ItemRemoved?.Invoke(key, item);
                return true;
            }
        }

        return false;
    }

    public void ForEach(Action<TKey, TItem> action)
    {
        foreach (var item in _items)
        {
            foreach (var value in item.Value)
            {
                action(item.Key, value);
            }
        }
    }

    public void ForEach(Func<TKey, TItem, bool> breaker)
    {
        foreach (var item in _items)
        {
            foreach (var value in item.Value)
            {
                if (breaker(item.Key, value)) break;
            }
        }
    }
}}