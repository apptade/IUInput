using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace SFInput.Screen {
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

        for (int i = 0; i < _clickInputs.Length; i++)
        {
            dictionary.Add(i, new(_clickInputs[i], MovementManager.DataManager.Data[0], AddableManager.DataManager.Data[i]));
        }

        return dictionary;
    }
}}