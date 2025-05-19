using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IUInput.Screen {
public sealed class ContactInputController : InputController, IDisposable
{
    private readonly ContactInputData _contactData;
    private readonly MovementInputData _movementData;
    private readonly ContactHandler[] _handlers;

    public Func<Vector2> CurrentPosition { get; }
    public Vector2? SettablePosition { get; private set; }

    public ContactInputController(string binding, ContactInputData contactData, MovementInputData movementData, Func<Vector2> positionFunc)
    {
        _contactData = contactData;
        _movementData = movementData;
        CurrentPosition = positionFunc;

        var contactInput = new InputAction(type : InputActionType.Button, binding: binding);
        var holdInput = new InputAction(type : InputActionType.Button, binding: binding, interactions: "hold");
        var multiTapInput = new InputAction(type : InputActionType.Button, binding: binding, interactions: "multiTap");
        var slowTapInput = new InputAction(type : InputActionType.Button, binding: binding, interactions: "slowTap");
        var tapInput = new InputAction(type : InputActionType.Button, binding: binding, interactions: "tap");

        _handlers = new ContactHandler[]
        {
            new(contactInput, this, contactData.Contact)
            {
                AdditionalStartAction = () => _contactData.Pressed.Value = true,
                AdditionalCancelAction = () => _contactData.Pressed.Value = false
            },
            new(holdInput, this, contactData.Hold) { AdditionalPerformPredicate = IsStaticContact },
            new(multiTapInput, this, contactData.MultiTap),
            new(slowTapInput, this, contactData.SlowTap) { AdditionalPerformPredicate = IsStaticContact },
            new(tapInput, this, contactData.Tap) { AdditionalPerformPredicate = IsStaticContact },
        };
    }

    public void Dispose()
    {
        foreach (var handler in _handlers) handler.Dispose();
    }

    protected override void OnEnable()
    {
        foreach (var handler in _handlers) handler.Enable();
    }

    protected override void OnDisable()
    {
        foreach (var handler in _handlers) handler.Disable();
    }

    private bool IsStaticContact(ContactHandler contactHandler)
    {
        var nullablePosition = _movementData.Position.Value;

        if (nullablePosition.HasValue)
        {
            return Vector2.Distance(contactHandler.StartPosition, nullablePosition.Value) < 0.1f;
        }

        return true;
    }

    private sealed class ContactHandler : InputActionHandler
    {
        private readonly ContactInputController _controller;
        private readonly IActionProperty<Vector2?> _property;

        public Action AdditionalStartAction { get; set; }
        public Action AdditionalCancelAction { get; set; }
        public Predicate<ContactHandler> AdditionalPerformPredicate { get; set; }

        public bool Pressed { get; private set; }
        public Vector2 StartPosition { get; private set; }

        public ContactHandler(InputAction action, ContactInputController controller, IActionProperty<Vector2?> property) : base(action)
        {
            _controller = controller;
            _property = property;
        }

        protected override void StartInput(InputAction.CallbackContext context)
        {
            StartPosition = _controller.CurrentPosition();
            _controller.SettablePosition = StartPosition;

            if (_controller.PredicateManager.AllResult())
            {
                Pressed = true;
                _property.OnStarted(StartPosition);
                AdditionalStartAction?.Invoke();
            }

            _controller.SettablePosition = null;
        }

        protected override void CancelInput(InputAction.CallbackContext context)
        {
            if (Pressed is false) return;

            Pressed = false;
            _property.OnCanceled(_controller.CurrentPosition());
            _property.Value = null;
            AdditionalCancelAction?.Invoke();
        }

        protected override void PerformInput(InputAction.CallbackContext context)
        {
            if (Pressed && AdditionalPerformPredicate?.Invoke(this) is true or null)
            {
                _controller.SettablePosition = _controller.CurrentPosition();

                if (_controller.PredicateManager.AllResult())
                {
                    _property.OnPerformed(_controller.SettablePosition);
                    _property.Value = null;
                }

                _controller.SettablePosition = null;
            }
        }
    }
}}