using System.Collections.Generic;

namespace IUInput {
public sealed class InputControllerManager<TController> : DoubleDictionaryHandler<int, TController>, IInputControllerManager<TController> where TController : IInputController
{
    public IReadOnlyDictionary<int, IReadOnlyList<TController>> Controllers { get => _source; }
}}