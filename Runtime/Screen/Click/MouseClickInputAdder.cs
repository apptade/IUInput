using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace AFUInput.Screen {
public sealed class MouseClickInputAdder : ClickInputAdder
{
    [SerializeField]
    private InputAction[] _clickInputs;

    protected override void Awake()
    {
        MovementManager.DataManager.AddData(0, 1);
        AddableManager.DataManager.AddData(0, _clickInputs.Length);
        base.Awake();
    }

    protected override IReadOnlyDictionary<int, ClickInputController> GetControllers()
    {
        var dictionary = new Dictionary<int, ClickInputController>(_clickInputs.Length);
        var movementData = MovementManager.DataManager.Data[0];

        for (int i = 0; i < _clickInputs.Length; i++)
        {
            var controller = new MouseClickInputController(_clickInputs[i], movementData, AddableManager.DataManager.Data[i]);
            dictionary.Add(i, controller);
        }

        return dictionary;
    }
}}