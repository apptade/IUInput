using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace IUInput.Screen {
public sealed class MouseClickInputAdder : ClickInputAdder
{
    [SerializeField]
    private InputAction[] _clickInputs;
    public IReadOnlyList<InputAction> ClickInputs { get => _clickInputs; }

    protected override IReadOnlyDictionary<int, ClickInputController> GetControllers()
    {
        var source = new Dictionary<int, ClickInputController>(_clickInputs.Length);
        var movementData = _movementManager.DataManager.GetData(0);

        for (int i = 0; i < _clickInputs.Length; i++)
        {
            var controller = new MouseClickInputController(_clickInputs[i], movementData, AddableManager.DataManager.GetData(i));
            source.Add(i, controller);
        }

        return source;
    }
}}