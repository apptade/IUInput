using UnityEngine;

namespace IUInput.Screen.Samples {
public sealed class ClickObjectPositioner : MonoBehaviour
{
    [SerializeField] InputManager<ClickInputController, ClickInputData> _clickManager;
    [SerializeField] Transform _bindingTarget;

    private void OnEnable()
    {
        _clickManager.DataManager.DataAdded += SubscribeClickData;
        _clickManager.DataManager.DataRemoved += UnsubscribeClickData;

        foreach (var data in _clickManager.DataManager.Data.Values) data.ClickUpChanged += OnAnyClick;
        _clickManager.DataManager.Data[0].ClickUpChanged += OnBindingClick;
    }

    private void OnDisable()
    {
        _clickManager.DataManager.DataAdded -= SubscribeClickData;
        _clickManager.DataManager.DataRemoved -= UnsubscribeClickData;

        foreach (var data in _clickManager.DataManager.Data.Values) data.ClickUpChanged -= OnAnyClick;
        _clickManager.DataManager.Data[0].ClickUpChanged -= OnBindingClick;
    }

    private void SubscribeClickData(int key, ClickInputData data)
    {
        data.ClickUpChanged += OnAnyClick;
    }

    private void UnsubscribeClickData(int key, ClickInputData data)
    {
        data.ClickUpChanged -= OnAnyClick;
    }

    private void OnAnyClick(Vector2 position)
    {
        if (GetTarget(position, out var target) && target != _bindingTarget)
        {
            target.rotation = GetRandomRotation();
            if (target.TryGetComponent(out MeshRenderer renderer)) renderer.material.color = GetRandomColor();
        }
    }

    private void OnBindingClick(Vector2 position)
    {
        if (GetTarget(position, out var target) && target == _bindingTarget)
        {
            target.rotation = GetRandomRotation();
            target.localScale = GetRandomScale();
        }
    }

    private bool GetTarget(in Vector2 screenPosition, out Transform target)
    {
        var camera = Camera.main;
        var worldPoint = camera.ScreenToWorldPoint(screenPosition);
        var hit = Physics.Raycast(worldPoint, camera.transform.forward, out RaycastHit hitInfo);

        target = hitInfo.transform;
        return hit;
    }

    private Color GetRandomColor()
    {
        var color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        return color;
    }

    private Vector3 GetRandomScale()
    {
        var scale = Random.Range(1f, 4f);
        return new(scale, scale, scale);
    }

    private Quaternion GetRandomRotation()
    {
        var rotation = Random.rotationUniform;
        return rotation;
    }
}}