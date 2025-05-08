using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace IUInput.Screen {
public sealed class MouseMovementInputAdder : MovementInputAdder
{
    [SerializeField, Space]
    private bool _movingOnlyWhenPressed;

    protected override IReadOnlyDictionary<int, MovementInputController> GetControllers()
    {
        CreateInputActions(out var deltaInput, out var positionInput);

        return new Dictionary<int, MovementInputController>
        {
            { 0, new(deltaInput, positionInput, AddableManager.DataManager.GetData(0)) }
        };
    }

    private void CreateInputActions(out InputAction deltaInput, out InputAction positionInput)
    {
        deltaInput = new InputAction();
        positionInput = new InputAction();

        if (_movingOnlyWhenPressed)
        {
            AddCompositeBinding(deltaInput, "<Mouse>/delta");
            AddCompositeBinding(positionInput, "<Mouse>/position");
        }
        else
        {
            deltaInput.AddBinding("<Mouse>/delta");
            positionInput.AddBinding("<Mouse>/position");
        }
    }

    private void AddCompositeBinding(InputAction inputAction, string binding)
    {
        inputAction.AddCompositeBinding("OneModifier")
            .With("Modifier", "<Mouse>/leftButton")
            .With("Modifier", "<Mouse>/rightButton")
            .With("Modifier", "<Mouse>/middleButton")
            .With("Modifier", "<Mouse>/forwardButton")
            .With("Modifier", "<Mouse>/backButton")
            .With("Binding", binding);
    }
}}