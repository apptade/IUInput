namespace SFInput {
public interface IInputManager<TData> where TData : IInputData
{
    IInputControllerManager<IInputController> ControllerManager { get; }
    IInputDataManager<TData> DataManager { get; }
}}