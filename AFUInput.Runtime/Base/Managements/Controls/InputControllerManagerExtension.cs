using System;
using System.Collections.Generic;

namespace AFUInput {
public static class InputControllerManagerExtension
{
    public static void AddControllers<TSource, TValue>(this IInputControllerManager<TSource> manager, IEnumerable<KeyValuePair<int, TValue>> controllers) where TSource : IInputController where TValue : TSource
    {
        foreach (var keyValuePair in controllers)
        {
            manager.AddController(keyValuePair.Key, keyValuePair.Value);
        }
    }

    public static void RemoveControllers<TSource, TValue>(this IInputControllerManager<TSource> manager, IEnumerable<KeyValuePair<int, TValue>> controllers) where TSource : IInputController where TValue : TSource
    {
        foreach (var keyValuePair in controllers)
        {
            manager.RemoveController(keyValuePair.Key, keyValuePair.Value);
        }
    }

    public static void ForEachController<TSource>(this IInputControllerManager<TSource> manager, Action<TSource> action) where TSource : IInputController
    {
        foreach (var controllers in manager.Controllers.Values)
        {
            foreach (var controller in controllers)
            {
                action(controller);
            }
        }
    }
}}