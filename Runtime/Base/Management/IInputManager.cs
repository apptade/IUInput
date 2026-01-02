namespace IUInput {
public interface IInputManager<TController, TData>
    where TController : IInputController
    where TData : IInputData
{
    IInputControllerManager<TController> ControllerManager { get; }
    IInputDataManager<TData> DataManager { get; }
}}