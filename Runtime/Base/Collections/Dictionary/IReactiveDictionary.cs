using System;

namespace IUInput {
public interface IReactiveDictionary<TKey, TItem>
{
    event Action<TKey, TItem> ItemAdded;
    event Action<TKey, TItem> ItemRemoved;

    bool Add(TKey key, TItem item);
    bool Clear();
    bool Remove(TKey key);

    void ForEach(Action<TKey, TItem> action);
    void ForEach(Func<TKey, TItem, bool> breaker);
}}