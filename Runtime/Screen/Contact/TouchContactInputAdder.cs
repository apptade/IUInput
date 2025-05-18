using UnityEngine;
using UnityEngine.InputSystem;

namespace IUInput.Screen {
public sealed class TouchContactInputAdder : ContactInputAdder
{
    [SerializeField, Range(1, 10)]
    private int _supportedFingersCount = 10;

    protected override void AddFirstControllers()
    {
        for (int i = 0; i < _supportedFingersCount; i++)
        {
            var binding = $"<Touchscreen>/touch{i}/press";
            var contactData = _addableManager.DataManager.GetData(i);
            var movementData = _movementManager.DataManager.GetData(i);

            var id = i;
            _controllerManager.AddValue(i, new(binding, contactData, movementData, () => GetCurrentPosition(id)));
        }
    }

    private Vector2 GetCurrentPosition(int id)
    {
        return Touchscreen.current.touches[id].position.value;
    }
}}