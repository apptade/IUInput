using UnityEngine;

namespace IUInput.Screen.Samples {
public sealed class PinchObjectScaler : MonoBehaviour
{
    [SerializeField] InputManager<PinchInputController, PinchInputData> _pinchManager;

    [Space]
    [SerializeField] Transform _target;
    [SerializeField] int _pinchId;

    [Space]
    [SerializeField] Vector3 _minTargetScale;
    [SerializeField] Vector3 _maxTargetScale;
    [SerializeField] float _targetScaleMultiplier;

    private Vector3 _targetScale;

    private void Awake()
    {
        _targetScale = _target.localScale;
    }

    private void OnEnable()
    {
        _pinchManager.DataManager.Data[_pinchId].PinchValueChanged += PinchTarget;
    }

    private void OnDisable()
    {
        _pinchManager.DataManager.Data[_pinchId].PinchValueChanged -= PinchTarget;
    }

    private void LateUpdate()
    {
        _target.localScale = Vector3.Lerp(_target.localScale, _targetScale, Time.deltaTime / 0.2f);
    }

    private void PinchTarget(float value)
    {
        var newScale = _targetScale + _targetScaleMultiplier * value * Vector3.one;
        _targetScale = ClampVector(newScale, _minTargetScale, _maxTargetScale);
    }

    private Vector3 ClampVector(in Vector3 value, in Vector3 min, in Vector3 max)
    {
        var x = Mathf.Clamp(value.x, min.x, max.x);
        var y = Mathf.Clamp(value.y, min.y, max.y);
        var z = Mathf.Clamp(value.z, min.z, max.z);

        return new Vector3(x, y, z);
    }
}}