namespace SFInput {
public static class InputDataManagerExtension
{
    public static void AddData<TData>(this IInputDataManager<TData> manager, int startIndex, int count) where TData : IInputData, new()
    {
        for (int i = startIndex; i < count; i++)
        {
            var data = new TData();
            manager.AddData(i, data);
        }
    }
}}