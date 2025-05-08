using UnityEngine.EventSystems;

namespace IUInput.Screen {
public sealed class PinchUIInputBreaker : UIInputBreaker<PinchInputController, PinchInputData>
{
    protected override UIPredicate<PinchInputController> InitializePredicate()
    {
        return new UIPinchPredicate(_eventSystem);
    }

    private sealed class UIPinchPredicate : UIPredicate<PinchInputController>
    {
        public UIPinchPredicate(EventSystem eventSystem) : base(eventSystem){}

        public override bool Result(PinchInputController entry)
        {
            var position = entry.SettableMiddlePosition;
            return position == null || IsPositionNotOverUI(position.Value);
        }
    }
}}