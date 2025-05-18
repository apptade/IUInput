namespace IUInput {
public interface IDoubleDictionaryHandler<TKey, TValue> : IDictionaryHandler<TKey, TValue>
{
    bool RemoveValue(TKey key, TValue value);
}}