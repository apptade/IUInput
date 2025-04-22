using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace SFInput.Screen {
public sealed class TouchClickInputAdder : ClickInputAdder
{
    public int SupportedFingersCount { get => 10; }

    protected override void Awake()
    {
        MovementManager.DataManager.AddData(0, SupportedFingersCount);
        AddableManager.DataManager.AddData(0, SupportedFingersCount);
        base.Awake();
    }

    protected override IReadOnlyDictionary<int, ClickInputController> GetControllers()
    {
        var dictionary = new Dictionary<int, ClickInputController>(SupportedFingersCount);

        for (int i = 0; i < SupportedFingersCount; i++)
        {
            var movementData = MovementManager.DataManager.Data[i];
            var positionFunc = new Func<Vector2>(() => Touchscreen.current.touches[i].position.value);

            var clickInput = new InputAction(type: InputActionType.Button, binding: $"<Touchscreen>/touch{i}/press");
            var controller = new ClickInputController(clickInput, movementData, positionFunc, AddableManager.DataManager.Data[i]);

            dictionary.Add(i, controller);
        }

        return dictionary;
    }
}}