using UnityEngine;

namespace IUInput.Screen {
public sealed class TouchPinchInputController : PinchInputController
{
    private readonly MovementInputData _movementData1;
    private readonly MovementInputData _movementData2;

    private Vector2? _lastDelta1;
    private Vector2? _lastDelta2;

    private int _pinchErrorNumber;
    private float _lastPinchDistance;

    public int MaxPinchErrorCount { get; set; }
    public float PinchSensitivity { get; set; }

    public TouchPinchInputController(MovementInputData movementData1, MovementInputData movementData2, PinchInputData pinchData) : base(pinchData)
    {
        _movementData1 = movementData1;
        _movementData2 = movementData2;
    }

    protected override void OnEnable()
    {
        _movementData1.MovementChanged += PerformPinchInput;
        _movementData1.Position.Canceled += CancelPinchInput;

        _movementData2.MovementChanged += PerformPinchInput;
        _movementData2.Position.Canceled += CancelPinchInput;
    }

    protected override void OnDisable()
    {
        _movementData1.MovementChanged -= PerformPinchInput;
        _movementData1.Position.Canceled -= CancelPinchInput;

        _movementData2.MovementChanged -= PerformPinchInput;
        _movementData2.Position.Canceled -= CancelPinchInput;
    }

    private void PerformPinchInput(Vector2 delta, Vector2 position)
    {
        if (CanPinch() is false) return;

        var position1 = _movementData1.Position.Value.Value;
        var position2 = _movementData2.Position.Value.Value;
        var pinchDistance = Vector2.Distance(position1, position2);

        if (_pinchErrorNumber++ > MaxPinchErrorCount)
        {
            var distanceDifference = Mathf.Abs(pinchDistance - _lastPinchDistance);
            if (distanceDifference < 0.1f) return;

            var magnitude = delta.magnitude / 10 * PinchSensitivity;
            SettableValue = pinchDistance > _lastPinchDistance ? magnitude : -magnitude;
            SettableMiddlePosition = Vector2.Lerp(position1, position2, 0.5f);

            if (PredicateManager.AllResult())
            {
                _pinchData.Pinch.Value = SettableValue;
                _pinchData.MiddlePosition.Value = SettableMiddlePosition;
                _pinchData.OnPinchChanged(SettableValue.Value, SettableMiddlePosition.Value);
            }

            SettableValue = null;
            SettableMiddlePosition = null;
        }

        _lastPinchDistance = pinchDistance;
    }

    private void CancelPinchInput(Vector2? position)
    {
        if (_pinchData.Pinch.Value.HasValue)
        {
            _pinchData.Pinch.Value = null;
            _pinchData.MiddlePosition.Value = null;
        }

        _lastDelta1 = null;
        _lastDelta2 = null;
        _pinchErrorNumber = 0;
    }

    private bool CanPinch()
    {
        var result = true;

        var bothFingersIsActive = _movementData1.Position.Value.HasValue && _movementData2.Position.Value.HasValue;
        if (bothFingersIsActive is false) return false;

        if (_lastDelta1 != null && _lastDelta2 != null)
        {
            if (Vector2.Dot(_lastDelta1.Value, _lastDelta2.Value) > -0.5f) result = false;
        }

        var delta1 = _movementData1.Delta.Value;
        var delta2 = _movementData2.Delta.Value;
        if (delta1 != null) _lastDelta1 = delta1.Value.normalized;
        if (delta2 != null) _lastDelta2 = delta2.Value.normalized;

        return result;
    }
}}