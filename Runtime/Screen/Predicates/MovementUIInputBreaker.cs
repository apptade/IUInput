using UnityEngine.EventSystems;

namespace IUInput.Screen {
public sealed class MovementUIInputBreaker : UIInputBreaker<MovementInputController, MovementInputData>
{
    protected override UIPredicate<MovementInputController> InitializePredicate()
    {
        return new UIMovementPredicate(_eventSystem);
    }

    private sealed class UIMovementPredicate : UIPredicate<MovementInputController>
    {
        public UIMovementPredicate(EventSystem eventSystem) : base(eventSystem){}

        public override bool Result(MovementInputController entry)
        {
            var position = entry.SettablePosition;
            if (position != null) return IsPositionNotOverUI(position.Value);
            else return entry.MovementData.Position.Value != null;
        }
    }
}}