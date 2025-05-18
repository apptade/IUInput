using UnityEngine.EventSystems;

namespace IUInput.Screen {
public sealed class PinchUIInputBreaker : UIInputBreaker<PinchInputController, PinchInputData>
{
    protected override UIPredicate<PinchInputController> CreatePredicate(PinchInputController controller)
    {
        return new PinchUIPredicate(controller, _eventSystem);
    }

    private sealed class PinchUIPredicate : UIPredicate<PinchInputController>
    {
        public PinchUIPredicate(PinchInputController controller, EventSystem eventSystem) : base(controller, eventSystem)
        {}

        public override bool Result()
        {
            var position = _controller.SettableMiddlePosition;
            return position.HasValue && IsPositionNotOverUI(position.Value);
        }
    }
}}