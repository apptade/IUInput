using UnityEngine;

namespace SFInput.Screen {
public sealed class TouchPinchInputController : PinchInputController
{
    private readonly ClickInputData _firstClickData;
    private readonly ClickInputData _secondClickData;

    private float _lastPinchDistance;
    private int _errorPinchNumber;
    private bool _pinchInputPerformed;

    public int MaxErrorPinchCount { get; set; } = 3;
    public float PinchSensitivity { get; set; } = 0.5f;

    public TouchPinchInputController(ClickInputData clickData1, ClickInputData clickData2, PinchInputData pinchData) : base(pinchData)
    {
        _firstClickData = clickData1;
        _secondClickData = clickData2;
    }

    protected override void OnEnable()
    {
        _firstClickData.ClickMovementChanged += PerformPinchInput;
        _firstClickData.ClickUpChanged += CancelPinchInput;

        _secondClickData.ClickMovementChanged += PerformPinchInput;
        _secondClickData.ClickUpChanged += CancelPinchInput;
    }

    protected override void OnDisable()
    {
        _firstClickData.ClickMovementChanged -= PerformPinchInput;
        _firstClickData.ClickUpChanged -= CancelPinchInput;

        _secondClickData.ClickMovementChanged -= PerformPinchInput;
        _secondClickData.ClickUpChanged -= CancelPinchInput;
    }

    private void PerformPinchInput(Vector2 delta, Vector2 position)
    {
        var position1 = _firstClickData.ClickPosition;
        var position2 = _secondClickData.ClickPosition;

        var middlePosition = Vector2.Lerp(position1, position2, 0.5f);
        SettableMiddlePosition = middlePosition;

        if (IsCanPinch() is false) return;

        var pinchDistance = Vector2.Distance(position1, position2);

        if (++_errorPinchNumber > MaxErrorPinchCount)
        {
            var pinchValue = pinchDistance > _lastPinchDistance ? PinchSensitivity : -PinchSensitivity;

            PinchData.OnPinchValueChanged(pinchValue);
            PinchData.OnPinchMiddlePositionChanged(middlePosition);
            PinchData.OnPinchChanged(pinchValue, middlePosition);

            _pinchInputPerformed = true;
        }

        _lastPinchDistance = pinchDistance;
    }

    private void CancelPinchInput(Vector2 position)
    {
        if (_pinchInputPerformed)
        {
            PinchData.OnPinchValueChanged(0);
            PinchData.OnPinchMiddlePositionChanged(Vector2.zero);

            _pinchInputPerformed = false;
            SettableMiddlePosition = Vector2.zero;
        }

        _errorPinchNumber = 0;
    }

    private bool IsCanPinch()
    {
        if (_firstClickData.Pressed is false || _secondClickData.Pressed is false) return false;

        var delta1 = _firstClickData.ClickDelta;
        var delta2 = _secondClickData.ClickDelta;
        if (delta1 != Vector2.zero && delta2 != Vector2.zero)
        {
            if (Vector2.Dot(delta1.normalized, delta2.normalized) > -0.9f) return false;
        }

        return PredicateManager.AllResult();
    }
}}