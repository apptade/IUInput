namespace IUInput {
public interface IReactiveDoubleDictionary<TKey, TItem> : IReactiveDictionary<TKey, TItem>
{
    bool Remove(TKey key, TItem item);
}}