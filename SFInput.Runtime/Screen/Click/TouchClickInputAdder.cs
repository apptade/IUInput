using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace SFInput.Screen {
public sealed class TouchClickInputAdder : ClickInputAdder
{
    public int SupportedFingersCount { get => 10; }

    protected override void Awake()
    {
        MovementManager.DataManager.AddData(0, SupportedFingersCount);
        AddableManager.DataManager.AddData(0, SupportedFingersCount);
        base.Awake();
    }

    protected override IReadOnlyDictionary<int, ClickInputController> GetControllers()
    {
        var dictionary = new Dictionary<int, ClickInputController>(SupportedFingersCount);

        for (int i = 0; i < SupportedFingersCount; i++)
        {
            var clickInput = new InputAction(type: InputActionType.Button, binding: $"<Touchscreen>/touch{i}/press");
            var controller = new ClickInputController(clickInput, MovementManager.DataManager.Data[i], AddableManager.DataManager.Data[i]);

            controller.PredicateManager.AddManager(MovementManager.ControllerManager.GetOrCreatePredicateManager(i));
            dictionary.Add(i, controller);
        }

        return dictionary;
    }
}}