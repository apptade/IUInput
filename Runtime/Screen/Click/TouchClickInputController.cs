using UnityEngine;
using UnityEngine.InputSystem;

namespace IUInput.Screen {
public sealed class TouchClickInputController : ClickInputController
{
    private readonly int _pointerId;

    public TouchClickInputController(InputAction clickInput, MovementInputData movementData, int pointerId, ClickInputData clickData) : base(clickInput, movementData, clickData)
    {
        _pointerId = pointerId;
    }

    protected override Vector2 GetCurrentPosition()
    {
        return Touchscreen.current.touches[_pointerId].position.value;
    }
}}