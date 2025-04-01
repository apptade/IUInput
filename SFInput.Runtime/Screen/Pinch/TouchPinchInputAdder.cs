using UnityEngine;
using System.Collections.Generic;

namespace SFInput.Screen {
public sealed class TouchPinchInputAdder : InputAdder<TouchPinchInputController, PinchInputData>
{
    [SerializeField] private InputManager<MovementInputData> _movementManager; 
    [SerializeField] private InputManager<ClickInputData> _clickManager;

    private int _controllerCount;

    protected override void Awake()
    {
        _controllerCount = 5;
        AddableManager.DataManager.AddData(0, _controllerCount);
        _movementManager.DataManager.AddData(0, _controllerCount * 2);
        _clickManager.DataManager.AddData(0, _controllerCount * 2);

        base.Awake();
    }

    protected override IReadOnlyDictionary<int, TouchPinchInputController> GetControllers()
    {
        var dictionary = new Dictionary<int, TouchPinchInputController>(_controllerCount);

        for (int i = 0, a = 0; i < _controllerCount * 2; i += 2, a++)
        {
            dictionary.Add(a, new
            (
                _movementManager.DataManager.Data[i],
                _movementManager.DataManager.Data[i + 1],
                _clickManager.DataManager.Data[i],
                _clickManager.DataManager.Data[i + 1],
                AddableManager.DataManager.Data[a]
            ));
        }

        return dictionary;
    }
}}