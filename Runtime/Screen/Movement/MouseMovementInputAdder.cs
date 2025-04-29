using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace IUInput.Screen {
public sealed class MouseMovementInputAdder : MovementInputAdder
{
    protected override void Awake()
    {
        AddableManager.DataManager.AddData(0, 1);
        base.Awake();
    }

    protected override IReadOnlyDictionary<int, MovementInputController> GetControllers()
    {
        var primaryDeltaInput = new InputAction(type: InputActionType.Value, binding: "<Mouse>/delta");
        var primaryPositionInput = new InputAction(type: InputActionType.Value, binding: "<Mouse>/position");

        return new Dictionary<int, MovementInputController>
        {
            { 0, new(primaryDeltaInput, primaryPositionInput, AddableManager.DataManager.Data[0]) }
        };
    }
}}