using UnityEngine;

namespace IUInput {
[DefaultExecutionOrder(-750)]
public abstract class InputAdder<TController, TData> : MonoBehaviour, IInputAdder<TController, TData> where TController : IInputController where TData : IInputData
{
    [SerializeField]
    protected InputManager<TController, TData> _addableManager;
    protected IInputControllerManager<TController> _controllerManager;

    public IInputManager<TController, TData> AddableManager { get => _addableManager; }
    public IInputControllerManager<TController> ControllerManager { get => _controllerManager; }

    protected virtual void Awake()
    {
        _controllerManager = new InputControllerManager<TController>();
        _controllerManager.ValueAdded += ConnectController;
        _controllerManager.ValueRemoved += UnconnectController;
        AddFirstControllers();
    }

    protected virtual void OnEnable()
    {
        _controllerManager.EnableAllControllers();
    }

    protected virtual void OnDisable()
    {
        _controllerManager.DisableAllControllers();
    }

    protected virtual void OnDestroy()
    {
        _controllerManager.ClearValues();
        _controllerManager.ValueAdded -= ConnectController;
        _controllerManager.ValueRemoved -= UnconnectController;
    }

    protected virtual void ConnectController(int key, TController controller)
    {
        _addableManager.ControllerManager.AddValue(key, controller);
    }

    protected virtual void UnconnectController(int key, TController controller)
    {
        _addableManager.ControllerManager.RemoveValue(key, controller);
    }

    protected abstract void AddFirstControllers();
}}