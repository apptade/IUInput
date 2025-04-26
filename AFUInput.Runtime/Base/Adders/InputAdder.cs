using UnityEngine;
using System.Collections.Generic;

namespace AFUInput {
[DefaultExecutionOrder(-750)]
public abstract class InputAdder<TController, TData> : MonoBehaviour, IInputAdder<TController, TData> where TController : IInputController where TData : IInputData
{
    [SerializeField]
    private InputManager<TController, TData> _addableManager;
    private IReadOnlyDictionary<int, TController> _controllers;

    public IInputManager<TController, TData> AddableManager { get => _addableManager; }
    public IReadOnlyDictionary<int, TController> Controllers { get => _controllers; }

    protected virtual void Awake()
    {
        _controllers = GetControllers();
        _addableManager.ControllerManager.AddControllers(Controllers);
    }

    protected virtual void OnEnable()
    {
        foreach (var controller in Controllers.Values) controller.Enable();
    }

    protected virtual void OnDisable()
    {
        foreach (var controller in Controllers.Values) controller.Disable();
    }

    protected virtual void OnDestroy()
    {
        _addableManager.ControllerManager.RemoveControllers(Controllers);
    }

    protected abstract IReadOnlyDictionary<int, TController> GetControllers();
}}