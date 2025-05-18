using UnityEngine;

namespace IUInput.Screen {
public sealed class ContactInputData : IInputData
{
    public readonly IValueProperty<bool> Pressed = new ValueProperty<bool>();

    public readonly IActionProperty<Vector2?> Contact = new ActionProperty<Vector2?>();
    public readonly IActionProperty<Vector2?> Hold = new ActionProperty<Vector2?>();
    public readonly IActionProperty<Vector2?> MultiTap = new ActionProperty<Vector2?>();
    public readonly IActionProperty<Vector2?> SlowTap = new ActionProperty<Vector2?>();
    public readonly IActionProperty<Vector2?> Tap = new ActionProperty<Vector2?>();
}}