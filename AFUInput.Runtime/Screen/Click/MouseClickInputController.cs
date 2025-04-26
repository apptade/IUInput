using UnityEngine;
using UnityEngine.InputSystem;

namespace SFInput.Screen {
public sealed class MouseClickInputController : ClickInputController
{
    public MouseClickInputController(InputAction clickInput, MovementInputData movementData, ClickInputData clickData) : base(clickInput, movementData, clickData)
    {
    }

    protected override Vector2 GetCurrentPosition()
    {
        return Mouse.current.position.value;
    }
}}