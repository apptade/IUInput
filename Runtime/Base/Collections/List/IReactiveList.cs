using System;

namespace IUInput {
public interface IReactiveList<TItem>
{
    event Action<TItem> ItemAdded;
    event Action<TItem> ItemRemoved;

    bool Add(TItem item);
    bool Clear();
    bool Remove(TItem item);

    void ForEach(Action<TItem> action);
    void ForEach(Func<TItem, bool> breaker);
}}