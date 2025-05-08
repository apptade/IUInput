using UnityEngine.EventSystems;

namespace IUInput.Screen {
public sealed class ClickUIInputBreaker : UIInputBreaker<ClickInputController, ClickInputData>
{
    protected override UIPredicate<ClickInputController> CreatePredicate(ClickInputController controller)
    {
        return new ClickUIPredicate(controller, _eventSystem);
    }

    private sealed class ClickUIPredicate : UIPredicate<ClickInputController>
    {
        public ClickUIPredicate(ClickInputController controller, EventSystem eventSystem) : base(controller, eventSystem){}

        public override bool Result()
        {
            var position = _controller.SettableStartPosition;
            return position == null || IsPositionNotOverUI(position.Value);
        }
    }
}}