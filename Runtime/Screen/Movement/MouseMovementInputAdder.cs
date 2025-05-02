using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace IUInput.Screen {
public sealed class MouseMovementInputAdder : InputAdder<MovementInputController, MovementInputData>
{
    protected override void OnDestroy()
    {
        foreach (var controller in Controllers.Values) controller.Dispose();
        base.OnDestroy();
    }

    protected override IReadOnlyDictionary<int, MovementInputController> GetControllers()
    {
        var deltaInput = new InputAction(type: InputActionType.Value, binding: "<Mouse>/delta");
        var positionInput = new InputAction(type: InputActionType.Value, binding: "<Mouse>/position");

        return new Dictionary<int, MovementInputController>
        {
            { 0, new(deltaInput, positionInput, AddableManager.DataManager.GetData(0)) }
        };
    }
}}