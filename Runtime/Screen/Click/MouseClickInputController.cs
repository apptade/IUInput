using UnityEngine;
using UnityEngine.InputSystem;

namespace IUInput.Screen {
public sealed class MouseClickInputController : ClickInputController
{
    public MouseClickInputController(InputAction clickInput, MovementInputData movementData, ClickInputData clickData) : base(clickInput, movementData, clickData){}

    protected override Vector2 CurrentPointerPosition()
    {
        return Mouse.current.position.value;
    }
}}