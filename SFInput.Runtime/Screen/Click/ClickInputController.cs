using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SFInput.Screen {
public sealed class ClickInputController : InputController, IDisposable
{
    private readonly ClickInputData _clickData;
    private readonly InputAction _clickInput;
    private readonly MovementInputData _movementData;

    private int _multipleClickCount;
    private float _multipleClickTimer;
    private bool _staticHoldLocked;

    public float MaxMultipleClickDuration { get; set; } = 0.25f;

    public ClickInputController(InputAction clickInput, MovementInputData movementData, ClickInputData clickData)
    {
        _clickInput = clickInput;
        _movementData = movementData;
        _clickData = clickData;
    }

    public void Dispose()
    {
        _clickInput.Dispose();
    }

    protected override void OnEnable()
    {
        _movementData.DeltaChanged += ChangeMovementDelta;
        _movementData.PositionChanged += ChangeMovementPosition;
        _movementData.MovementChanged += ChangeMovement;

        _clickInput.performed += PerformClickInput;
        _clickInput.canceled += CancelClickInput;
        _clickInput.Enable();
    }

    protected override void OnDisable()
    {
        _movementData.DeltaChanged -= ChangeMovementDelta;
        _movementData.PositionChanged -= ChangeMovementPosition;
        _movementData.MovementChanged -= ChangeMovement;

        _clickInput.performed -= PerformClickInput;
        _clickInput.canceled -= CancelClickInput;
        _clickInput.Disable();
    }

    private void ChangeMovementDelta(Vector2 delta)
    {
        if (_clickData.Pressed)
        {
            _clickData.OnClickDeltaChanged(delta);
        }
    }

    private void ChangeMovementPosition(Vector2 position)
    {
        if (_clickData.Pressed)
        {
            _clickData.OnClickPositionChanged(position);
        }
    }

    private void ChangeMovement(Vector2 delta, Vector2 position)
    {
        if (_clickData.Pressed)
        {
            _clickData.OnClickMovementChanged(delta, position);
        }
    }

    private void PerformClickInput(InputAction.CallbackContext callback)
    {
        if (PredicateManager.Result())
        {
            _clickData.OnClickDownChanged(_movementData.Position);
            ChangeMovementPosition(_movementData.Position);
        }
    }

    private void CancelClickInput(InputAction.CallbackContext callback)
    {
        if (_clickData.Pressed is false) return;

        _clickData.OnClickUpChanged(_movementData.Position);

        if (IsVectorsMatch(_clickData.ClickDownPosition, _clickData.ClickUpPosition))
        {
            _multipleClickTimer = 0;
            _clickData.OnStaticClickChanged(_movementData.Position, _multipleClickCount++);
        }

        _staticHoldLocked = false;
        _clickData.OnStaticHoldTimeChanged(0);
        _clickData.OnClickDeltaChanged(Vector2.zero);
        _clickData.OnClickPositionChanged(Vector2.zero);
    }

    public void FixedUpdate()
    {
        UpdateMultipleClickCount();
        UpdateClickStaticHoldTime();
    }

    private void UpdateMultipleClickCount()
    {
        if (_multipleClickCount is 0) return;

        _multipleClickTimer += Time.fixedDeltaTime;
        if (_multipleClickTimer > MaxMultipleClickDuration)
        {
            _multipleClickCount = 0;
            _multipleClickTimer = 0;
        }
    }

    private void UpdateClickStaticHoldTime()
    {
        if (_clickData.Pressed is false || _staticHoldLocked) return;

        if (IsVectorsMatch(_clickData.ClickDownPosition, _movementData.Position))
        {
            var holdTime = _clickData.StaticHoldTime + Time.fixedDeltaTime;
            _clickData.OnStaticHoldTimeChanged(holdTime);
        }
        else
        {
            _staticHoldLocked = true;
            _clickData.OnStaticHoldTimeChanged(0);
        }
    }

    private bool IsVectorsMatch(in Vector2 vector1, in Vector2 vector2)
    {
        var distance = Vector2.Distance(vector1, vector2);
        return distance < 5;
    }
}}