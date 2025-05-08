using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace IUInput.Screen {
public sealed class TouchMovementInputAdder : MovementInputAdder
{
    protected override IReadOnlyDictionary<int, MovementInputController> GetControllers()
    {
        var supportedFingersCount = 10;
        var source = new Dictionary<int, MovementInputController>(supportedFingersCount);

        for (int i = 0; i < supportedFingersCount; i++)
        {
            var deltaInput = new InputAction();
            deltaInput.AddCompositeBinding("OneModifier")
                .With("Modifier", $"<Touchscreen>/touch{i}/press")
                .With("Binding", $"<Touchscreen>/touch{i}/delta");

            var positionInput = new InputAction();
            positionInput.AddCompositeBinding("OneModifier")
                .With("Modifier", $"<Touchscreen>/touch{i}/press")
                .With("Binding", $"<Touchscreen>/touch{i}/position");

            source.Add(i, new(deltaInput, positionInput, AddableManager.DataManager.GetData(i)));
        }

        return source;
    }
}}