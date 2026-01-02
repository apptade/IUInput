using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace IUInput.Screen {
public sealed class CustomMovementInputAdder : MovementInputAdder
{
    [SerializeField]
    private CustomDataInput[] _customInputs;
    public IReadOnlyList<CustomDataInput> CustomInputs { get => _customInputs; }

    protected override void AddFirstControllers()
    {
        var length = _customInputs.Length;

        for (int i = 0; i < length; i++)
        {
            var input = _customInputs[i];
            _controllerManager.Add(i, new(input.DeltaInput, input.PositionInput, _addableManager.DataManager.GetOrCreateData(i)));
        }
    }

    [Serializable]
    public sealed class CustomDataInput
    {
        [SerializeField] private InputAction _deltaInput;
        [SerializeField] private InputAction _positionInput;

        public InputAction DeltaInput { get => _deltaInput; }
        public InputAction PositionInput { get => _positionInput; }
    }
}}