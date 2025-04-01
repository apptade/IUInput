using UnityEngine;

namespace SFInput.Screen {
public sealed class TouchPinchInputController : InputController
{
    private readonly MovementInputData _firstMovementData;
    private readonly MovementInputData _secondMovementData;
    private readonly ClickInputData _firstClickData;
    private readonly ClickInputData _secondClickData;
    private readonly PinchInputData _pinchData;

    private int _errorPinchValue;
    private float _lastPinchDistance;

    public int MaxErrorPinchCount { get; set; } = 1;
    public float PinchSensitivity { get; set; } = 0.5f;

    public TouchPinchInputController(MovementInputData movementData1, MovementInputData movementData2, ClickInputData clickData1, ClickInputData clickData2, PinchInputData pinchData)
    {
        _firstMovementData = movementData1;
        _secondMovementData = movementData2;
        _firstClickData = clickData1;
        _secondClickData = clickData2;
        _pinchData = pinchData;
    }

    protected override void OnEnable()
    {
        _firstClickData.ClickUpChanged += OnAnyClickUpChanged;
        _firstMovementData.PositionChanged += OnAnyPositionChanged;

        _secondClickData.ClickUpChanged += OnAnyClickUpChanged;
        _secondMovementData.PositionChanged += OnAnyPositionChanged;
    }

    protected override void OnDisable()
    {
        _firstClickData.ClickUpChanged -= OnAnyClickUpChanged;
        _firstMovementData.PositionChanged -= OnAnyPositionChanged;

        _secondClickData.ClickUpChanged -= OnAnyClickUpChanged;
        _secondMovementData.PositionChanged -= OnAnyPositionChanged;
    }

    private void OnAnyClickUpChanged(Vector2 position)
    {
        _errorPinchValue = 0;
    }

    private void OnAnyPositionChanged(Vector2 position)
    {
        if (PredicateManager.Result() is false) return;
        if (_firstClickData.Pressed is false || _secondClickData.Pressed is false) return;

        _errorPinchValue++;

        var position1 = _firstMovementData.Position;
        var position2 = _secondMovementData.Position;
        var pinchDistance = Vector2.Distance(position1, position2);

        if (_errorPinchValue > MaxErrorPinchCount)
        {
            var middlePosition = Vector2.Lerp(position1, position2, 0.5f);

            if (pinchDistance > _lastPinchDistance)
            {
                _pinchData.OnPinchChanged(PinchSensitivity, middlePosition);
            }
            else if (pinchDistance < _lastPinchDistance)
            {
                _pinchData.OnPinchChanged(-PinchSensitivity, middlePosition);
            }
        }

        _lastPinchDistance = pinchDistance;
    }
}}