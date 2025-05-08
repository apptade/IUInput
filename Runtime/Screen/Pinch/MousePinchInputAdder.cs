using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace IUInput.Screen {
public sealed class MousePinchInputAdder : InputAdder<PinchInputController, PinchInputData>
{
    [SerializeField, Space]
    private InputAction[] _customScrollInputs;
    public IReadOnlyList<InputAction> CustomScrollInputs { get => _customScrollInputs; }

    protected override void OnDestroy()
    {
        foreach (var controller in Controllers.Values)
        {
            if (controller is IDisposable disposable) disposable.Dispose();
        }

        base.OnDestroy();
    }

    protected override IReadOnlyDictionary<int, PinchInputController> GetControllers()
    {
        if (_customScrollInputs.Length > 0)
        {
            var length = _customScrollInputs.Length;
            var source = new Dictionary<int, PinchInputController>(length);

            for (int i = 0; i < length; i++)
            {
                source.Add(i, new MousePinchInputController(_customScrollInputs[i], AddableManager.DataManager.GetData(i)));
            }

            return source;
        }
        else
        {
            var pinchInput = new InputAction(type: InputActionType.Value, binding: "<Mouse>/scroll");

            return new Dictionary<int, PinchInputController>()
            {
                { 0, new MousePinchInputController(pinchInput, AddableManager.DataManager.GetData(0)) }
            };
        }
    }
}}