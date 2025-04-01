using UnityEngine;
using System.Collections.Generic;

namespace SFInput {
[DefaultExecutionOrder(-750)]
public abstract class InputAdder<TController, TData> : MonoBehaviour, IInputAdder<TController, TData> where TController : IInputController where TData : IInputData
{
    [SerializeField]
    private InputManager<TData> _addableManager;
    private IReadOnlyDictionary<int, TController> _controllers;

    public IInputManager<TData> AddableManager { get => _addableManager; }
    public IReadOnlyDictionary<int, TController> Controllers { get => _controllers; }

    protected virtual void Awake()
    {
        _controllers = GetControllers();
        _addableManager.ControllerManager.AddControllers(_controllers);
    }

    protected virtual void OnEnable()
    {
        this.ForEachController(c => c.Enable());
    }

    protected virtual void OnDisable()
    {
        this.ForEachController(c => c.Disable());
    }

    protected virtual void OnDestroy()
    {
        _addableManager.ControllerManager.RemoveControllers(_controllers);
    }

    protected abstract IReadOnlyDictionary<int, TController> GetControllers();
}}