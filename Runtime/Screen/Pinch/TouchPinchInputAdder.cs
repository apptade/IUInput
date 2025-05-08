using UnityEngine;
using System.Collections.Generic;

namespace IUInput.Screen {
public sealed class TouchPinchInputAdder : InputAdder<PinchInputController, PinchInputData>
{
    [SerializeField]
    private InputManager<MovementInputController, MovementInputData> _movementManager;

    [Space]
    [SerializeField, Range(1, 10)] private int _maxPinchErrorCount;
    [SerializeField, Range(0.05f, 1)] private float _pinchSensitivity;

    protected override IReadOnlyDictionary<int, PinchInputController> GetControllers()
    {
        var supportedFingersCount = 10;
        var source = new Dictionary<int, PinchInputController>(supportedFingersCount / 2);

        for (int i = 0, a = 0; i < supportedFingersCount; i += 2, a++)
        {
            source.Add(a, new TouchPinchInputController
            (
                _movementManager.DataManager.GetData(i),
                _movementManager.DataManager.GetData(i + 1),
                AddableManager.DataManager.GetData(a)
            )
            {
                MaxPinchErrorCount = _maxPinchErrorCount,
                PinchSensitivity = _pinchSensitivity
            });
        }

        return source;
    }
}}