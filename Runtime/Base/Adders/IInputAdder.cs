using System.Collections.Generic;

namespace IUInput {
public interface IInputAdder<TController, TData> where TController : IInputController where TData : IInputData
{
    IInputManager<TController, TData> AddableManager { get; }
    IReadOnlyDictionary<int, TController> Controllers { get; }
}}