using System;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace IUInput.Screen {
public sealed class MousePinchInputAdder : InputAdder<PinchInputController, PinchInputData>
{
    protected override void OnDestroy()
    {
        foreach (var controller in Controllers.Values)
        {
            if (controller is IDisposable disposable) disposable.Dispose(); 
        }

        base.OnDestroy();
    }

    protected override IReadOnlyDictionary<int, PinchInputController> GetControllers()
    {
        var pinchInput = new InputAction(type: InputActionType.Value, binding: "<Mouse>/scroll");
        var controller = new MousePinchInputController(pinchInput, AddableManager.DataManager.GetData(0));

        return new Dictionary<int, PinchInputController>()
        {
            { 0, controller }
        };
    }
}}