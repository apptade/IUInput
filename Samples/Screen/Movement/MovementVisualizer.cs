using UnityEngine;
using System.Collections.Generic;

namespace IUInput.Screen.Samples {
public sealed class MovementVisualizer : MonoBehaviour
{
    [SerializeField] InputManager<MovementInputController, MovementInputData> _movementManager;

    [Space]
    [SerializeField] GUIStyle _movementGUIStyle;
    [SerializeField, Range(0.1f, 1)] float _textTransparencyMultiplier;
    [SerializeField, Range(0.2f, 5)] float _textTransparencyStartTime;

    private Dictionary<int, GUIMovementEffect> _effects;

    private void Awake()
    {
        _effects = new();
    }

    private void OnGUI()
    {
        foreach (var effect in _effects.Values) effect.Draw();
    }

    private void OnEnable()
    {
        foreach (var keyValuePair in _movementManager.DataManager.Data)
        {
            AddEffect(keyValuePair.Key, keyValuePair.Value);
        }

        _movementManager.DataManager.DataAdded += AddEffect;
        _movementManager.DataManager.DataRemoved += RemoveEffect;
    }

    private void OnDisable()
    {
        _effects.Clear();
        _movementManager.DataManager.DataAdded -= AddEffect;
        _movementManager.DataManager.DataRemoved -= RemoveEffect;
    }

    private void AddEffect(int key, MovementInputData data)
    {
        var effect = new GUIMovementEffect(key.ToString(), new(_movementGUIStyle), data)
        {
            TextTransparencyMultiplier = _textTransparencyMultiplier,
            TextTransparencyStartTime = _textTransparencyStartTime
        };

        _effects.Add(key, effect);
    }

    private void RemoveEffect(int key, MovementInputData data)
    {
        _effects.Remove(key);
    }

    private sealed class GUIMovementEffect
    {
        private readonly string _keyText;
        private readonly GUIStyle _gUIStyle;
        private readonly MovementInputData _movementData;

        private Vector2 _lastPosition;
        private float _textTransparencyStartTimer;

        public float TextTransparencyMultiplier { get; set; }
        public float TextTransparencyStartTime { get; set; }

        public GUIMovementEffect(string keyText, GUIStyle gUIStyle, MovementInputData movementData)
        {
            _keyText = keyText;
            _gUIStyle = gUIStyle;
            _movementData = movementData;
        }

        public void Draw()
        {
            var positionX = _movementData.Position.x;
            var positionY = UnityEngine.Screen.height - _movementData.Position.y;
            var rect = new Rect(positionX, positionY, 1, 1);

            var keyText = $"{_keyText} id";
            var positionText = $"<size={_gUIStyle.fontSize / 2}>x:{positionX} y:{positionY}</size>";

            _gUIStyle.normal.textColor = GetNormalTextColor();
            GUI.Label(rect, $"{keyText}\n{positionText}", _gUIStyle);
        }

        private Color GetNormalTextColor()
        {
            var color = _gUIStyle.normal.textColor;

            if (Vector2.Distance(_movementData.Position, _lastPosition) > 0.1f)
            {
                color.a = 1;
                _textTransparencyStartTimer = 0;
                _lastPosition = _movementData.Position;
            }
            else
            {
                _textTransparencyStartTimer += Time.unscaledDeltaTime;
                if (_textTransparencyStartTimer > TextTransparencyStartTime)
                {
                    var delta = Time.unscaledDeltaTime * TextTransparencyMultiplier;
                    color.a = Mathf.Clamp(color.a - delta, 0, 1);
                }
            }

            return color;
        }
    }
}}