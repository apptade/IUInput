using UnityEngine;
using UnityEngine.InputSystem;

namespace IUInput.Screen {
public sealed class MouseClickInputController : ClickInputController
{
    public override Vector2 PointerPosition { get => Mouse.current.position.value; }
    public MouseClickInputController(InputAction clickInput, MovementInputData movementData, ClickInputData clickData) : base(clickInput, movementData, clickData){}
}}