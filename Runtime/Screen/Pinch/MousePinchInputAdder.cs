using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace IUInput.Screen {
public sealed class MousePinchInputAdder : InputAdder<PinchInputController, PinchInputData>
{
    protected override void OnDestroy()
    {
        foreach (var controller in Controllers.Values) controller.Dispose();
        base.OnDestroy();
    }

    protected override IReadOnlyDictionary<int, PinchInputController> GetControllers()
    {
        var dictionary = new Dictionary<int, PinchInputController>(1);
        var pinchInput = new InputAction(type: InputActionType.Value, binding: "<Mouse>/scroll");
        var controller = new MousePinchInputController(pinchInput, AddableManager.DataManager.GetData(0));

        dictionary.Add(0, controller);
        return dictionary;
    }
}}