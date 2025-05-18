using UnityEngine;

namespace IUInput.Screen {
public abstract class ContactInputAdder : InputAdder<ContactInputController, ContactInputData>
{
    [SerializeField]
    protected InputManager<MovementInputController, MovementInputData> _movementManager;

    protected override void UnconnectController(int key, ContactInputController controller)
    {
        base.UnconnectController(key, controller);
        controller.Dispose();
    }
}}