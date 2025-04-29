using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IUInput.Screen {
public abstract class ClickInputController : InputController, IDisposable
{
    private readonly InputAction _clickInput;

    public readonly ClickInputData ClickData;
    public readonly MovementInputData MovementData;

    public Vector2 SettableClickDownPosition { get; private set; }
    public bool ClickPressed { get; private set; }
    public bool StaticHoldLocked { get; private set; }
    public int MultipleClickCount { get; private set; }
    public float MultipleClickTimer { get; private set; }

    public float MaxMultipleClickDuration { get; set; } = 0.25f;

    public ClickInputController(InputAction clickInput, MovementInputData movementData, ClickInputData clickData)
    {
        _clickInput = clickInput;
        MovementData = movementData;
        ClickData = clickData;
    }

    public void Dispose()
    {
        _clickInput.Dispose();
    }

    protected override void OnEnable()
    {
        MovementData.DeltaChanged += ChangeClickDelta;
        MovementData.PositionChanged += ChangeClickPosition;
        MovementData.MovementChanged += ChangeClickMovement;

        _clickInput.started += StartClickInput;
        _clickInput.canceled += CancelClickInput;
        _clickInput.Enable();
    }

    protected override void OnDisable()
    {
        MovementData.DeltaChanged -= ChangeClickDelta;
        MovementData.PositionChanged -= ChangeClickPosition;
        MovementData.MovementChanged -= ChangeClickMovement;

        _clickInput.started -= StartClickInput;
        _clickInput.canceled -= CancelClickInput;
        _clickInput.Disable();
    }

    private void ChangeClickDelta(Vector2 delta)
    {
        if (ClickPressed) ClickData.OnClickDeltaChanged(delta);
    }

    private void ChangeClickPosition(Vector2 position)
    {
        if (ClickPressed) ClickData.OnClickPositionChanged(position);
    }

    private void ChangeClickMovement(Vector2 delta, Vector2 position)
    {
        if (ClickPressed) ClickData.OnClickMovementChanged(delta, position);
    }

    private void StartClickInput(InputAction.CallbackContext callback)
    {
        SettableClickDownPosition = GetCurrentPosition();

        if (PredicateManager.AllResult())
        {
            ClickPressed = true;
            ClickData.OnClickDownChanged(SettableClickDownPosition);
            ChangeClickPosition(SettableClickDownPosition);
        }
    }

    private void CancelClickInput(InputAction.CallbackContext callback)
    {
        if (ClickPressed is false) return;

        ClickData.OnClickUpChanged(MovementData.Position);

        if (IsVectorsMatch(ClickData.ClickDownPosition, ClickData.ClickUpPosition))
        {
            MultipleClickTimer = 0;
            ClickData.OnStaticClickChanged(MovementData.Position, MultipleClickCount++);
        }

        ClickPressed = false;
        StaticHoldLocked = false;
        SettableClickDownPosition = Vector2.zero;

        ClickData.OnStaticHoldTimeChanged(0);
        ClickData.OnClickDeltaChanged(Vector2.zero);
        ClickData.OnClickPositionChanged(Vector2.zero);
    }

    public void FixedUpdate()
    {
        UpdateMultipleClickCount();
        UpdateClickStaticHoldTime();
    }

    private void UpdateMultipleClickCount()
    {
        if (MultipleClickCount is 0) return;

        MultipleClickTimer += Time.fixedDeltaTime;
        if (MultipleClickTimer > MaxMultipleClickDuration)
        {
            MultipleClickCount = 0;
            MultipleClickTimer = 0;
        }
    }

    private void UpdateClickStaticHoldTime()
    {
        if (ClickPressed is false || StaticHoldLocked) return;

        if (IsVectorsMatch(ClickData.ClickDownPosition, MovementData.Position))
        {
            var holdTime = ClickData.StaticHoldTime + Time.fixedDeltaTime;
            ClickData.OnStaticHoldTimeChanged(holdTime);
        }
        else
        {
            StaticHoldLocked = true;
            ClickData.OnStaticHoldTimeChanged(0);
        }
    }

    private bool IsVectorsMatch(in Vector2 vector1, in Vector2 vector2)
    {
        var distance = Vector2.Distance(vector1, vector2);
        return distance < 5;
    }

    protected abstract Vector2 GetCurrentPosition();
}}