using System;

namespace IUInput {
public static class InputDataManagerExtension
{
    public static T GetOrCreateData<T>(this IInputDataManager<T> manager, int key) where T : IInputData, new()
    {
        if (manager.Data.TryGetValue(key, out var value)) return value;

        var data = new T();
        if (manager.Add(key, data)) return data;

        throw new OperationCanceledException();
    }
}}