using UnityEngine;
using UnityEngine.InputSystem;

namespace IUInput.Screen {
public sealed class MouseContactInputAdder : ContactInputAdder
{
    protected override void AddFirstControllers()
    {
        var bindings = GetBindingPaths();

        for (int i = 0; i < bindings.Length; i++)
        {
            var contactData = _addableManager.DataManager.GetOrCreateData(i);
            var movementData = _movementManager.DataManager.GetOrCreateData(0);

            _controllerManager.Add(i, new(bindings[i], contactData, movementData, GetCurrentPosition));
        }
    }

    private string[] GetBindingPaths()
    {
        return new string[]
        {
            "<Mouse>/leftButton",
            "<Mouse>/rightButton",
            "<Mouse>/middleButton",
            "<Mouse>/forwardButton",
            "<Mouse>/backButton"
        };
    }

    private Vector2 GetCurrentPosition()
    {
        return Mouse.current.position.value;
    }
}}