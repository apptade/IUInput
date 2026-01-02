using UnityEngine.EventSystems;

namespace IUInput.Screen {
public sealed class ContactUIInputBreaker : UIInputBreaker<ContactInputController, ContactInputData>
{
    protected override UIPredicate<ContactInputController> CreatePredicate(ContactInputController controller)
    {
        return new ContactUIPredicate(controller, _eventSystem);
    }

    private sealed class ContactUIPredicate : UIPredicate<ContactInputController>
    {
        public ContactUIPredicate(ContactInputController controller, EventSystem eventSystem) : base(controller, eventSystem)
        {}

        public override bool Result()
        {
            var position = _controller.SettablePosition;
            return position.HasValue && IsPositionNotOverUI(position.Value);
        }
    }
}}