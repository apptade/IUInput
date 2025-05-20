using UnityEngine;

namespace IUInput.Screen.Samples {
public sealed class MovementTrailEffect : MonoBehaviour
{
    [SerializeField] private InputManager<MovementInputController, MovementInputData> _movementManager;
    [SerializeField] private int _movementDataKey;
    [SerializeField] private TrailRenderer _trail;

    private void OnEnable()
    {
        _movementManager.DataManager.GetData(_movementDataKey).Position.Started += TeleportTrail;
        _movementManager.DataManager.GetData(_movementDataKey).Position.Performed += MoveTrail;
    }

    private void OnDisable()
    {
        _movementManager.DataManager.GetData(_movementDataKey).Position.Started -= TeleportTrail;
        _movementManager.DataManager.GetData(_movementDataKey).Position.Performed -= MoveTrail;
    }

    private void TeleportTrail(Vector2? position)
    {
        _trail.transform.position = ScreenToWorldPosition(position.Value);
        _trail.Clear();
    }

    private void MoveTrail(Vector2? position)
    {
        _trail.transform.position = ScreenToWorldPosition(position.Value);
    }

    private Vector2 ScreenToWorldPosition(in Vector2 screenPosition)
    {
        return Camera.main.ScreenToWorldPoint(screenPosition);
    }
}}