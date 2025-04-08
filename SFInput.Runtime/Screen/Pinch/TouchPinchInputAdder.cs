using UnityEngine;
using System.Collections.Generic;

namespace SFInput.Screen {
public sealed class TouchPinchInputAdder : InputAdder<TouchPinchInputController, PinchInputData>
{
    [SerializeField]
    private InputManager<ClickInputData> _clickManager;
    public int SupportedFingersCount { get => 10; }

    protected override void Awake()
    {
        AddableManager.DataManager.AddData(0, SupportedFingersCount / 2);
        _clickManager.DataManager.AddData(0, SupportedFingersCount);
        base.Awake();
    }

    protected override IReadOnlyDictionary<int, TouchPinchInputController> GetControllers()
    {
        var dictionary = new Dictionary<int, TouchPinchInputController>(SupportedFingersCount);

        for (int i = 0, a = 0; i < SupportedFingersCount; i += 2, a++)
        {
            dictionary.Add(a, new(
                _clickManager.DataManager.Data[i], 
                _clickManager.DataManager.Data[i + 1], 
                AddableManager.DataManager.Data[a]
            ));
        }

        return dictionary;
    }
}}