using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace IUInput.Screen {
public sealed class TouchClickInputAdder : ClickInputAdder
{
    protected override IReadOnlyDictionary<int, ClickInputController> GetControllers()
    {
        var supportedFingersCount = 10;
        var source = new Dictionary<int, ClickInputController>(supportedFingersCount);

        for (int i = 0; i < supportedFingersCount; i++)
        {
            var movementData = _movementManager.DataManager.GetData(i);
            var clickInput = new InputAction(type: InputActionType.Button, binding: $"<Touchscreen>/touch{i}/press");
            var controller = new TouchClickInputController(clickInput, movementData, pointerId: i, AddableManager.DataManager.GetData(i));

            source.Add(i, controller);
        }

        return source;
    }
}}