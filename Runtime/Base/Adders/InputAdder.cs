using UnityEngine;

namespace IUInput {
[DefaultExecutionOrder(-750)]
public abstract class InputAdder<TController, TData> : MonoBehaviour, IInputAdder<TController, TData>
    where TController : IInputController
    where TData : IInputData
{
    [SerializeField]
    protected InputManager<TController, TData> _addableManager;
    protected IInputControllerManager<TController> _controllerManager;

    public IInputManager<TController, TData> AddableManager { get => _addableManager; }
    public IInputControllerManager<TController> ControllerManager { get => _controllerManager; }

    protected virtual void Awake()
    {
        _controllerManager = new InputControllerManager<TController>();
        _controllerManager.ItemAdded += ConnectController;
        _controllerManager.ItemRemoved += UnconnectController;
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
        _controllerManager.Clear();
        _controllerManager.ItemAdded -= ConnectController;
        _controllerManager.ItemRemoved -= UnconnectController;
    }

    protected virtual void ConnectController(int key, TController controller)
    {
        _addableManager.ControllerManager.Add(key, controller);
    }

    protected virtual void UnconnectController(int key, TController controller)
    {
        _addableManager.ControllerManager.Remove(key, controller);
    }

    protected abstract void AddFirstControllers();
}}