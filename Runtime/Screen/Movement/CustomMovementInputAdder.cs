using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace IUInput.Screen {
public sealed class CustomMovementInputAdder : MovementInputAdder
{
    [SerializeField, Space]
    private CustomDataInput[] _customInputs;
    public IReadOnlyList<CustomDataInput> CustomInputs { get => _customInputs; }

    protected override IReadOnlyDictionary<int, MovementInputController> GetControllers()
    {
        var length = _customInputs.Length;
        var source = new Dictionary<int, MovementInputController>(length);

        for (int i = 0; i < length; i++)
        {
            var input = _customInputs[i];
            source.Add(i, new(input.DeltaInput, input.PositionInput, AddableManager.DataManager.GetData(i)));
        }

        return source;
    }

    [Serializable]
    public sealed class CustomDataInput
    {
        [SerializeField] InputAction _deltaInput;
        [SerializeField] InputAction _positionInput;

        public InputAction DeltaInput { get => _deltaInput; }
        public InputAction PositionInput { get => _positionInput; }
    }
}}