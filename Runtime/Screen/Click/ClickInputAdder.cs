using UnityEngine;

namespace AFUInput.Screen {
public abstract class ClickInputAdder : InputAdder<ClickInputController, ClickInputData>
{
    [SerializeField]
    private InputManager<MovementInputController, MovementInputData> _movementManager;
    public InputManager<MovementInputController, MovementInputData> MovementManager { get => _movementManager; }

    protected override void OnDestroy()
    {
        foreach (var controller in Controllers.Values) controller.Dispose();
        base.OnDestroy();
    }

    protected virtual void FixedUpdate()
    {
        foreach (var controller in Controllers.Values) controller.FixedUpdate();
    }
}}