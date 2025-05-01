namespace IUInput {
public static class InputDataManagerExtension
{
    public static TData GetData<TData>(this IInputDataManager<TData> manager, in int key) where TData : IInputData, new()
    {
        if (manager.Data.TryGetValue(key, out var value)) return value;

        var data = new TData();
        if (manager.AddData(key, data)) return data;

        return default;
    }
}}