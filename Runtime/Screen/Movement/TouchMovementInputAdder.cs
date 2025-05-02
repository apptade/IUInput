using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace IUInput.Screen {
public sealed class TouchMovementInputAdder : InputAdder<MovementInputController, MovementInputData>
{
    protected override void OnDestroy()
    {
        foreach (var controller in Controllers.Values) controller.Dispose();
        base.OnDestroy();
    }

    protected override IReadOnlyDictionary<int, MovementInputController> GetControllers()
    {
        var supportedFingersCount = 10;
        var dictionary = new Dictionary<int, MovementInputController>(supportedFingersCount);

        for (int i = 0; i < supportedFingersCount; i++)
        {
            var deltaInput = new InputAction(type: InputActionType.Value, binding: $"<Touchscreen>/touch{i}/delta");
            var positionInput = new InputAction(type: InputActionType.Value, binding: $"<Touchscreen>/touch{i}/position");

            dictionary.Add(i, new(deltaInput, positionInput, AddableManager.DataManager.GetData(i)));
        }

        return dictionary;
    }
}}