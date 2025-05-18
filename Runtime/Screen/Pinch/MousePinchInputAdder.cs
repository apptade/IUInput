using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace IUInput.Screen {
public sealed class MousePinchInputAdder : InputAdder<PinchInputController, PinchInputData>
{
    [SerializeField]
    private InputAction[] _customScrollInputs;
    public IReadOnlyList<InputAction> CustomScrollInputs { get => _customScrollInputs; }

    protected override void AddFirstControllers()
    {
        var scrollInputs = GetScrollInputs();

        for (int i = 0; i < scrollInputs.Count; i++)
        {
            _controllerManager.AddValue(i, new MousePinchInputController(scrollInputs[i], _addableManager.DataManager.GetData(i)));
        }
    }

    protected override void UnconnectController(int key, PinchInputController controller)
    {
        base.UnconnectController(key, controller);
        if (controller is MousePinchInputController disposable) disposable.Dispose();
    }

    private IReadOnlyList<InputAction> GetScrollInputs()
    {
        if (_customScrollInputs.Length > 0) return _customScrollInputs;
        else return new InputAction[] { new(type: InputActionType.Value, binding: "<Mouse>/scroll") };
    }
}}