namespace IUInput {
public static class InputControllerManagerExtension
{
    public static void DisableAllControllers<T>(this IInputControllerManager<T> manager) where T : IInputController
    {
        manager.ForEach(DisableNow);

        static void DisableNow(int key, T controller)
        {
            controller.Disable();
        }
    }

    public static void EnableAllControllers<T>(this IInputControllerManager<T> manager) where T : IInputController
    {
        manager.ForEach(EnableNow);

        static void EnableNow(int key, T controller)
        {
            controller.Enable();
        }
    }
}}