using System.Collections.Generic;

namespace IUInput {
public interface IInputControllerManager<TController> : IDoubleDictionaryHandler<int, TController> where TController : IInputController
{
    IReadOnlyDictionary<int, IReadOnlyList<TController>> Controllers { get; }
}}