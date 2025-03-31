using System;

namespace SFInput {
public static class InputAdderExtension
{
    public static void ForEachController<TController, TData>(this IInputAdder<TController, TData> adder, Action<TController> action) where TController : IInputController where TData : IInputData
    {
        foreach (var controller in adder.Controllers.Values)
        {
            action(controller);
        }
    }
}}