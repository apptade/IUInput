using System;
using System.Collections.Generic;

namespace IUInput {
public class ReactiveList<TItem> : IReactiveList<TItem>
{
    protected readonly List<TItem> _items;

    public event Action<TItem> ItemAdded;
    public event Action<TItem> ItemRemoved;

    public ReactiveList(List<TItem> items = null)
    {
        _items = items ?? new();
    }

    public bool Add(TItem item)
    {
        if (item is null) return false;

        _items.Add(item);
        ItemAdded?.Invoke(item);

        return true;
    }

    public bool Clear()
    {
        if (_items.Count is 0) return false;

        if (ItemRemoved is not null)
        {
            var memorySource = new List<TItem>(_items);
            _items.Clear();

            foreach (var item in memorySource)
            {
                ItemRemoved.Invoke(item);
            }
        }
        else _items.Clear();

        return true;
    }

    public bool Remove(TItem item)
    {
        if (_items.Remove(item))
        {
            ItemRemoved?.Invoke(item);
            return true;
        }

        return false;
    }

    public void ForEach(Action<TItem> action)
    {
        foreach (var item in _items) action(item);
    }

    public void ForEach(Func<TItem, bool> breaker)
    {
        foreach (var item in _items)
        {
            if (breaker(item)) break;
        }
    }
}}