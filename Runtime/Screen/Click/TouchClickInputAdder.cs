using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace IUInput.Screen {
public sealed class TouchClickInputAdder : ClickInputAdder
{
    public int SupportedFingersCount { get => 10; }

    protected override IReadOnlyDictionary<int, ClickInputController> GetControllers()
    {
        var dictionary = new Dictionary<int, ClickInputController>(SupportedFingersCount);

        for (int i = 0; i < SupportedFingersCount; i++)
        {
            var movementData = MovementManager.DataManager.Data[i];
            var clickInput = new InputAction(type: InputActionType.Button, binding: $"<Touchscreen>/touch{i}/press");
            var controller = new TouchClickInputController(clickInput, movementData, pointerId: i, AddableManager.DataManager.GetData(i));

            dictionary.Add(i, controller);
        }

        return dictionary;
    }
}}