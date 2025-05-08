using UnityEngine.EventSystems;

namespace IUInput.Screen {
public sealed class ClickUIInputBreaker : UIInputBreaker<ClickInputController, ClickInputData>
{
    protected override UIPredicate<ClickInputController> InitializePredicate()
    {
        return new UIClickPredicate(_eventSystem);
    }

    private sealed class UIClickPredicate : UIPredicate<ClickInputController>
    {
        public UIClickPredicate(EventSystem eventSystem) : base(eventSystem){}

        public override bool Result(ClickInputController entry)
        {
            var position = entry.SettableStartPosition;
            return position == null || IsPositionNotOverUI(position.Value);
        }
    }
}}