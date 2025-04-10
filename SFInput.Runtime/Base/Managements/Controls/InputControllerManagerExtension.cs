using System.Collections.Generic;

namespace SFInput {
public static class InputControllerManagerExtension
{
    public static void AddControllers<TSource, TValue>(this IInputControllerManager<TSource> manager, IEnumerable<KeyValuePair<int, TValue>> controllers) where TSource : IInputController where TValue : TSource
    {
        foreach (var keyValuePair in controllers)
        {
            manager.AddController(keyValuePair.Key, keyValuePair.Value);
        }
    }

    public static void AddPredicates<TSource>(this IInputControllerManager<TSource> manager, IReadOnlyList<IInputPredicate> predicates) where TSource : IInputController
    {
        for (int i = 0; i < predicates.Count; i++)
        {
            manager.GetOrCreatePredicateManager(i).AddPredicate(predicates[i]);
        }
    }

    public static IInputPredicateManager GetOrCreatePredicateManager<TSource>(this IInputControllerManager<TSource> manager, int index) where TSource : IInputController
    {
        if (manager.PredicateManagers.TryGetValue(index, out var predicateManager))
        {
            return predicateManager;
        }
        else
        {
            manager.AddPredicateManager(index, new InputPredicateManager());
            return manager.PredicateManagers[index];
        }
    }

    public static void RemoveControllers<TSource, TValue>(this IInputControllerManager<TSource> manager, IEnumerable<KeyValuePair<int, TValue>> controllers) where TSource : IInputController where TValue : TSource
    {
        foreach (var keyValuePair in controllers)
        {
            manager.RemoveController(keyValuePair.Key, keyValuePair.Value);
        }
    }

    public static void RemovePredicates<TSource>(this IInputControllerManager<TSource> manager, IReadOnlyList<IInputPredicate> predicates) where TSource : IInputController
    {
        for (int i = 0; i < predicates.Count; i++)
        {
            if (manager.PredicateManagers.TryGetValue(i, out var predicateManager))
            {
                predicateManager.RemovePredicate(predicates[i]);
            }
        }
    }
}}