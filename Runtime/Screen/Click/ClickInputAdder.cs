using UnityEngine;

namespace IUInput.Screen {
public abstract class ClickInputAdder : InputAdder<ClickInputController, ClickInputData>
{
    [SerializeField]
    protected InputManager<MovementInputController, MovementInputData> _movementManager;

    [Space]
    [SerializeField] protected bool _blockErrorMovement;
    [SerializeField, Range(0, 25)] protected float _maxErrorMovementCount;
    [SerializeField, Range(0.1f, 1)] protected float _maxMultipleClickDuration;

    protected override void Awake()
    {
        base.Awake();
        UpdateControllersSettings();
    }

    protected override void OnDestroy()
    {
        foreach (var controller in Controllers.Values) controller.Dispose();
        base.OnDestroy();
    }

    protected virtual void OnValidate()
    {
        UpdateControllersSettings();
    }

    protected virtual void FixedUpdate()
    {
        foreach (var controller in Controllers.Values) controller.FixedUpdate();
    }

    protected virtual void UpdateControllersSettings()
    {
        if (Controllers is null) return;

        foreach (var controller in Controllers.Values)
        {
            controller.BlockErrorMovement = _blockErrorMovement;
            controller.MaxErrorMovementCount = _maxErrorMovementCount;
            controller.MaxMultipleClickDuration = _maxMultipleClickDuration;
        }
    }
}}