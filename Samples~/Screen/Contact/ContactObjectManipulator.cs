using UnityEngine;

namespace IUInput.Screen.Samples {
public sealed class ContactObjectManipulator : MonoBehaviour
{
    [SerializeField] private InputManager<ContactInputController, ContactInputData> _contactManager;
    [SerializeField] private int _contactDataKey;
    [SerializeField] MeshRenderer _target;

    private void OnEnable()
    {
        _contactManager.DataManager.GetData(_contactDataKey).Hold.Performed += ChangeToWhiteColor;
        _contactManager.DataManager.GetData(_contactDataKey).MultiTap.Performed += RandomizeColor;
        _contactManager.DataManager.GetData(_contactDataKey).SlowTap.Performed += RandomizeScale;
        _contactManager.DataManager.GetData(_contactDataKey).Tap.Performed += RandomizeRotation;
    }

    private void OnDisable()
    {
        _contactManager.DataManager.GetData(_contactDataKey).Hold.Performed -= ChangeToWhiteColor;
        _contactManager.DataManager.GetData(_contactDataKey).MultiTap.Performed -= RandomizeColor;
        _contactManager.DataManager.GetData(_contactDataKey).SlowTap.Performed -= RandomizeScale;
        _contactManager.DataManager.GetData(_contactDataKey).Tap.Performed -= RandomizeRotation;
    }

    private void ChangeToWhiteColor(Vector2? position)
    {
        _target.material.color = Color.white;
    }

    private void RandomizeColor(Vector2? position)
    {
        _target.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.25f, 1f);
    }

    private void RandomizeRotation(Vector2? position)
    {
        _target.transform.rotation = Random.rotationUniform;
    }

    private void RandomizeScale(Vector2? position)
    {
        _target.transform.localScale += Vector3.one * Random.Range(-0.5f, 0.5f);
    }
}}