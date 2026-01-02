using UnityEngine;

namespace IUInput {
[DefaultExecutionOrder(-1000)]
public abstract class InputManager<TController, TData> : MonoBehaviour, IInputManager<TController, TData>
    where TController : IInputController
    where TData : IInputData
{
    public IInputControllerManager<TController> ControllerManager { get; private set; }
    public IInputDataManager<TData> DataManager { get; private set; }

    protected virtual void Awake()
    {
        ControllerManager = new InputControllerManager<TController>();
        DataManager = new InputDataManager<TData>();
    }
}}