namespace AFUInput {
public static class InputDataManagerExtension
{
    public static void AddData<TSource>(this IInputDataManager<TSource> manager, int startIndex, int count) where TSource : IInputData, new()
    {
        for (int i = startIndex; i < count; i++)
        {
            if (manager.Data.ContainsKey(i)) continue;

            var data = new TSource();
            manager.AddData(i, data);
        }
    }
}}