using UnityEngine;

namespace IUInput.Screen {
public abstract class MovementInputAdder : InputAdder<MovementInputController, MovementInputData>
{
    [SerializeField, Range(0, 50)]
    protected int _maxMovementErrorCount;

    protected override void ConnectController(int key, MovementInputController controller)
    {
        base.ConnectController(key, controller);
        controller.MaxMovementErrorCount = _maxMovementErrorCount;
    }

    protected override void UnconnectController(int key, MovementInputController controller)
    {
        base.UnconnectController(key, controller);
        controller.Dispose();
    }
}}