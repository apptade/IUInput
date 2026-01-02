using UnityEngine;

namespace IUInput.Screen {
public sealed class TouchPinchInputAdder : InputAdder<PinchInputController, PinchInputData>
{
    [SerializeField]
    private InputManager<MovementInputController, MovementInputData> _movementManager;

    [Space]
    [SerializeField, Range(1, 10)] private int _maxPinchErrorCount;
    [SerializeField, Range(1, 5)] private int _maxPinchFingersCount;
    [SerializeField, Range(0.05f, 1)] private float _pinchSensitivity;

    protected override void AddFirstControllers()
    {
        for (int i = 0, a = 0; a < _maxPinchFingersCount; i += 2, a++)
        {
            _controllerManager.Add(a, new TouchPinchInputController
            (
                _movementManager.DataManager.GetOrCreateData(i),
                _movementManager.DataManager.GetOrCreateData(i + 1),
                _addableManager.DataManager.GetOrCreateData(a)
            )
            {
                MaxPinchErrorCount = _maxPinchErrorCount,
                PinchSensitivity = _pinchSensitivity
            });
        }
    }
}}