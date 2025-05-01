using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace IUInput.Screen {
public sealed class TouchMovementInputAdder : MovementInputAdder
{
    public int SupportedFingersCount { get => 10; }

    protected override IReadOnlyDictionary<int, MovementInputController> GetControllers()
    {
        var dictionary = new Dictionary<int, MovementInputController>(SupportedFingersCount);

        for (int i = 0; i < SupportedFingersCount; i++)
        {
            var deltaInput = new InputAction(type: InputActionType.Value, binding: $"<Touchscreen>/touch{i}/delta");
            var positionInput = new InputAction(type: InputActionType.Value, binding: $"<Touchscreen>/touch{i}/position");

            dictionary.Add(i, new(deltaInput, positionInput, AddableManager.DataManager.GetData(i)));
        }

        return dictionary;
    }
}}