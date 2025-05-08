using UnityEngine;
using UnityEngine.InputSystem;

namespace IUInput.Screen {
public sealed class TouchClickInputController : ClickInputController
{
    private readonly int _pointerId;
    public override Vector2 PointerPosition { get => Touchscreen.current.touches[_pointerId].position.value; }

    public TouchClickInputController(InputAction clickInput, MovementInputData movementData, int pointerId, ClickInputData clickData) : base(clickInput, movementData, clickData)
    {
        _pointerId = pointerId;
    }
}}