using UnityEngine;
using System.Collections.Generic;

namespace AFUInput.Screen {
public sealed class TouchPinchInputAdder : InputAdder<PinchInputController, PinchInputData>
{
    [SerializeField]
    private InputManager<ClickInputController, ClickInputData> _clickManager;
    public int SupportedFingersCount { get => 10; }

    protected override void Awake()
    {
        AddableManager.DataManager.AddData(0, SupportedFingersCount / 2);
        _clickManager.DataManager.AddData(0, SupportedFingersCount);
        base.Awake();
    }

    protected override IReadOnlyDictionary<int, PinchInputController> GetControllers()
    {
        var dictionary = new Dictionary<int, PinchInputController>(SupportedFingersCount / 2);

        for (int i = 0, a = 0; i < SupportedFingersCount; i += 2, a++)
        {
            dictionary.Add(a, new TouchPinchInputController(
                _clickManager.DataManager.Data[i], 
                _clickManager.DataManager.Data[i + 1], 
                AddableManager.DataManager.Data[a]
            ));
        }

        return dictionary;
    }
}}