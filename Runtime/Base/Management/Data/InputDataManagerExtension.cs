using System;

namespace IUInput {
public static class InputDataManagerExtension
{
    public static T GetData<T>(this IInputDataManager<T> manager, in int key) where T : IInputData, new()
    {
        if (manager.Data.TryGetValue(key, out var value)) return value;

        var data = new T();
        if (manager.AddValue(key, data)) return data;

        throw new NullReferenceException();
    }
}}