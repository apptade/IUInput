using System;

namespace IUInput {
public static class InputControllerManagerExtension
{
    public static void DisableAllControllers<T>(this IInputControllerManager<T> manager) where T : IInputController
    {
        foreach (var controllers in manager.Controllers.Values)
        {
            foreach (var controller in controllers)
            {
                controller.Disable();
            }
        }
    }

    public static void EnableAllControllers<T>(this IInputControllerManager<T> manager) where T : IInputController
    {
        foreach (var controllers in manager.Controllers.Values)
        {
            foreach (var controller in controllers)
            {
                controller.Enable();
            }
        }
    }

    public static void ForEachController<T>(this IInputControllerManager<T> manager, Action<T> action) where T : IInputController
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