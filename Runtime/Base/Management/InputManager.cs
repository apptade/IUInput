using UnityEngine;

namespace IUInput {
[DefaultExecutionOrder(-1000)]
public abstract class InputManager<TController, TData> : MonoBehaviour, IInputManager<TController, TData> where TController : IInputController where TData : IInputData
{
    private IInputControllerManager<TController> _controllerManager;
    private IInputDataManager<TData> _dataManager;

    public IInputControllerManager<TController> ControllerManager { get => _controllerManager; }
    public IInputDataManager<TData> DataManager { get => _dataManager; }

    protected virtual void Awake()
    {
        _controllerManager = new InputControllerManager<TController>();
        _dataManager = new InputDataManager<TData>();
    }
}}