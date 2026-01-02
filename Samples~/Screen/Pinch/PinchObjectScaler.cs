using UnityEngine;

namespace IUInput.Screen.Samples {
public sealed class PinchObjectScaler : MonoBehaviour
{
    [SerializeField] private InputManager<PinchInputController, PinchInputData> _pinchManager;
    [SerializeField] private int _pinchDataKey;
    [Space]
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _minTargetScale;
    [SerializeField] private Vector3 _maxTargetScale;
    [SerializeField] private float _targetScaleMultiplier;

    private Vector3 _targetScale;

    private void Awake()
    {
        _targetScale = _target.localScale;
    }

    private void OnEnable()
    {
        _pinchManager.DataManager.GetOrCreateData(_pinchDataKey).PinchChanged += PinchTarget;
    }

    private void OnDisable()
    {
        _pinchManager.DataManager.GetOrCreateData(_pinchDataKey).PinchChanged -= PinchTarget;
    }

    private void LateUpdate()
    {
        _target.localScale = Vector3.Lerp(_target.localScale, _targetScale, Time.deltaTime / 0.2f);
    }

    private void PinchTarget(float value, Vector2 middlePosition)
    {
        var newScale = _targetScale + _targetScaleMultiplier * value * Vector3.one;
        _targetScale = ClampVector(newScale, _minTargetScale, _maxTargetScale);
    }

    private Vector3 ClampVector(Vector3 value, Vector3 min, Vector3 max)
    {
        var x = Mathf.Clamp(value.x, min.x, max.x);
        var y = Mathf.Clamp(value.y, min.y, max.y);
        var z = Mathf.Clamp(value.z, min.z, max.z);

        return new Vector3(x, y, z);
    }
}}