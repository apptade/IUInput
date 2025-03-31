using UnityEngine;

namespace SFInput {
[DefaultExecutionOrder(-1000)]
public abstract class InputManager<TData> : MonoBehaviour, IInputManager<TData> where TData : IInputData
{
    private IInputControllerManager<IInputController> _controllerManager;
    private IInputDataManager<TData> _dataManager;

    public IInputControllerManager<IInputController> ControllerManager { get => _controllerManager; }
    public IInputDataManager<TData> DataManager { get => _dataManager; }

    protected virtual void Awake()
    {
        _controllerManager = new InputControllerManager<IInputController>();
        _dataManager = new InputDataManager<TData>();
    }
}}