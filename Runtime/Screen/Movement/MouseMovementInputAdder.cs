using UnityEngine;
using UnityEngine.InputSystem;

namespace IUInput.Screen {
public sealed class MouseMovementInputAdder : MovementInputAdder
{
    [SerializeField]
    private bool _movingOnlyWhenPressed = true;

    protected override void AddFirstControllers()
    {
        CreateInputActions(out var deltaInput, out var positionInput);
        _controllerManager.AddValue(0, new(deltaInput, positionInput, _addableManager.DataManager.GetData(0)));
    }

    private void CreateInputActions(out InputAction deltaInput, out InputAction positionInput)
    {
        deltaInput = new();
        positionInput = new();

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

        static void AddCompositeBinding(InputAction action, string binding)
        {
            string[] modifiers = new string[]
            {
                "<Mouse>/leftButton",
                "<Mouse>/rightButton",
                "<Mouse>/middleButton",
                "<Mouse>/forwardButton",
                "<Mouse>/backButton",
            };

            foreach (var modifier in modifiers)
            {
                action.AddCompositeBinding("OneModifier")
                    .With("Modifier", modifier)
                    .With("Binding", binding);
            }
        }
    }
}}