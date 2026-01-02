using UnityEngine.EventSystems;

namespace IUInput.Screen {
public sealed class MovementUIInputBreaker : UIInputBreaker<MovementInputController, MovementInputData>
{
    protected override UIPredicate<MovementInputController> CreatePredicate(MovementInputController controller)
    {
        return new MovementUIPredicate(controller, _eventSystem);
    }

    private sealed class MovementUIPredicate : UIPredicate<MovementInputController>
    {
        public MovementUIPredicate(MovementInputController controller, EventSystem eventSystem) : base(controller, eventSystem)
        {}

        public override bool Result()
        {
            var position = _controller.SettablePosition;
            return position.HasValue && IsPositionNotOverUI(position.Value);
        }
    }
}}