using UnityEngine;
using UnityEngine.InputSystem;

namespace IUInput.Screen {
public sealed class TouchMovementInputAdder : MovementInputAdder
{
    [SerializeField, Range(1, 10)]
    private int _supportedFingersCount = 10;

    protected override void AddFirstControllers()
    {
        for (int i = 0; i < _supportedFingersCount; i++)
        {
            var deltaInput = new InputAction(binding: $"<Touchscreen>/touch{i}/delta");
            var positionInput = new InputAction();
            positionInput.AddCompositeBinding("OneModifier")
                .With("Modifier", $"<Touchscreen>/touch{i}/press")
                .With("Binding", $"<Touchscreen>/touch{i}/position");

            _controllerManager.AddValue(i, new(deltaInput, positionInput, _addableManager.DataManager.GetData(i)));
        }
    }
}}