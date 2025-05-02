using UnityEngine;

namespace IUInput.Screen {
public sealed class TouchPinchInputController : PinchInputController
{
    private readonly ClickInputData _firstClickData;
    private readonly ClickInputData _secondClickData;

    private float _lastPinchDistance;
    private int _errorPinchNumber;

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
        if (CanPinch())
        {
            var position1 = _firstClickData.ClickPosition;
            var position2 = _secondClickData.ClickPosition;
            var pinchDistance = Vector2.Distance(position1, position2);

            if (++_errorPinchNumber > MaxErrorPinchCount)
            {
                SettableValue = pinchDistance > _lastPinchDistance ? PinchSensitivity : -PinchSensitivity;
                SettableMiddlePosition = Vector2.Lerp(position1, position2, 0.5f);

                if (PredicateManager.AllResult())
                {
                    _pinchData.OnPinchValueChanged(SettableValue.Value);
                    _pinchData.OnPinchMiddlePositionChanged(SettableMiddlePosition.Value);
                    _pinchData.OnPinchChanged(SettableValue.Value, SettableMiddlePosition.Value);
                }
            }

            _lastPinchDistance = pinchDistance;
        }

        SettableValue = null;
        SettableMiddlePosition = null;
    }

    private void CancelPinchInput(Vector2 position)
    {
        if (_pinchData.PinchValue is not 0)
        {
            _pinchData.OnPinchValueChanged(0);
            _pinchData.OnPinchMiddlePositionChanged(Vector2.zero);
        }

        _errorPinchNumber = 0;
    }

    private bool CanPinch()
    {
        if (_firstClickData.Pressed is false || _secondClickData.Pressed is false) return false;

        var delta1 = _firstClickData.ClickDelta;
        var delta2 = _secondClickData.ClickDelta;

        if (delta1 == Vector2.zero || delta2 == Vector2.zero) return false;
        if (Vector2.Dot(delta1.normalized, delta2.normalized) > -0.9f) return false;

        return true;
    }
}}