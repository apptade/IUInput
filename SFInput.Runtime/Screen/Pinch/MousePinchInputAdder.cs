using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace SFInput.Screen {
public sealed class MousePinchInputAdder : InputAdder<MousePinchInputController, PinchInputData>
{
    [SerializeField]
    private InputManager<MovementInputData> _movementManager;

    protected override void Awake()
    {
        AddableManager.DataManager.AddData(0, 1);
        base.Awake();
    }

    protected override void OnDestroy()
    {
        foreach (var controller in Controllers.Values) controller.Dispose();
        base.OnDestroy();
    }

    protected override IReadOnlyDictionary<int, MousePinchInputController> GetControllers()
    {
        var dictionary = new Dictionary<int, MousePinchInputController>(1);
        var pinchInput = new InputAction(type: InputActionType.Value, binding: "<Mouse>/scroll");
        var controller = new MousePinchInputController(pinchInput, AddableManager.DataManager.Data[0]);

        controller.PredicateManager.AddManager(_movementManager.ControllerManager.GetOrCreatePredicateManager(0));
        dictionary.Add(0, controller);

        return dictionary;
    }
}}