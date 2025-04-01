using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SFInput.Screen {
public sealed class ClickInputController : InputController, IDisposable
{
    private readonly ClickInputData _clickData;
    private readonly InputAction _clickInput;
    private readonly MovementInputData _movementData;

    private bool _clickedDown;
    private int _multipleClickCount;
    private float _multipleClickTimer;
    private float _staticPressTime;

    public float MaxMultipleClickTime { get; set; } = 0.25f;

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
        _clickInput.started += OnClickInputStarted;
        _clickInput.canceled += OnClickInputCanceled;
        _clickInput.Enable();
    }

    protected override void OnDisable()
    {
        _clickInput.started -= OnClickInputStarted;
        _clickInput.canceled -= OnClickInputCanceled;
        _clickInput.Disable();
    }

    private void OnClickInputStarted(InputAction.CallbackContext callback)
    {
        if (PredicateManager.Result() is false) return;

        _clickData.OnClickDownChanged(_movementData.Position);
        _clickedDown = true;
    }

    private void OnClickInputCanceled(InputAction.CallbackContext callback)
    {
        if (_clickedDown is false) return;

        _clickData.OnClickUpChanged(_movementData.Position);
        _clickedDown = false;

        var distance = Vector2.Distance(_clickData.ClickDownPosition, _clickData.ClickUpPosition);
        if (distance < 3)
        {
            _multipleClickTimer = 0;
            _clickData.OnClickChanged(_movementData.Position, _multipleClickCount++);
        }

        _staticPressTime = 0;
        _clickData.OnStaticPressTimeChanged(time:0);
    }

    public void FixedUpdate()
    {
        UpdateMultipleClick();
        UpdateStaticPressTime();
    }

    private void UpdateMultipleClick()
    {
        if (_multipleClickCount > 0)
        {
            _multipleClickTimer += Time.fixedDeltaTime;
            if (_multipleClickTimer > MaxMultipleClickTime)
            {
                _multipleClickCount = 0;
                _multipleClickTimer = 0;
            }
        }
    }

    private void UpdateStaticPressTime()
    {
        if (_clickedDown)
        {
            var distance = Vector2.Distance(_clickData.ClickDownPosition, _movementData.Position);
            if (distance < 3)
            {
                _staticPressTime += Time.fixedDeltaTime;
                _clickData.OnStaticPressTimeChanged(_staticPressTime);
            }
        }
    }
}}