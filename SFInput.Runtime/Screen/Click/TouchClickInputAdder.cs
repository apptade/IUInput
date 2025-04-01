using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace SFInput.Screen {
public sealed class TouchClickInputAdder : ClickInputAdder
{
    private int _controllerCount;

    protected override void Awake()
    {
        _controllerCount = 10;
        MovementManager.DataManager.AddData(0, _controllerCount);
        AddableManager.DataManager.AddData(0, _controllerCount);
        base.Awake();
    }

    protected override IReadOnlyDictionary<int, ClickInputController> GetControllers()
    {
        var primaryClickInput = new InputAction(type: InputActionType.Button, binding: "<Touchscreen>/primaryTouch/press");

        var dictionary = new Dictionary<int, ClickInputController>(_controllerCount)
        {
            { 0, new(primaryClickInput, MovementManager.DataManager.Data[0], AddableManager.DataManager.Data[0]) }
        };

        for (int i = 1; i < _controllerCount; i++)
        {
            var clickInput = new InputAction(type: InputActionType.Button, binding: $"<Touchscreen>/touch{i}/press");
            dictionary.Add(i, new(clickInput, MovementManager.DataManager.Data[i], AddableManager.DataManager.Data[i]));
        }

        return dictionary;
    }
}}