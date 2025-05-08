using UnityEngine;

namespace IUInput.Screen {
public abstract class ClickInputAdder : InputAdder<ClickInputController, ClickInputData>
{
    [SerializeField]
    protected InputManager<MovementInputController, MovementInputData> _movementManager;

    [SerializeField, Space, Range(0.1f, 1)]
    protected float _maxMultipleClickDuration;

    protected override void Awake()
    {
        base.Awake();
        UpdateControllersSettings();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        foreach (var controller in Controllers.Values) controller.Dispose();
    }

    protected virtual void FixedUpdate()
    {
        foreach (var controller in Controllers.Values) controller.FixedUpdate();
    }

    protected virtual void UpdateControllersSettings()
    {
        foreach (var controller in Controllers.Values)
        {
            controller.MaxMultipleClickDuration = _maxMultipleClickDuration;
        }
    }
}}