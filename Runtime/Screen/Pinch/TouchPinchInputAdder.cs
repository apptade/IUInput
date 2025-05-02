using UnityEngine;
using System.Collections.Generic;

namespace IUInput.Screen {
public sealed class TouchPinchInputAdder : InputAdder<PinchInputController, PinchInputData>
{
    [SerializeField]
    private InputManager<ClickInputController, ClickInputData> _clickManager;

    protected override IReadOnlyDictionary<int, PinchInputController> GetControllers()
    {
        var supportedFingersCount = 10;
        var dictionary = new Dictionary<int, PinchInputController>(supportedFingersCount / 2);

        for (int i = 0, a = 0; i < supportedFingersCount; i += 2, a++)
        {
            dictionary.Add(a, new TouchPinchInputController
            (
                _clickManager.DataManager.GetData(i),
                _clickManager.DataManager.GetData(i + 1),
                AddableManager.DataManager.GetData(a)
            ));
        }

        return dictionary;
    }
}}