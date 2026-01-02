using System.Collections.Generic;

namespace IUInput {
public sealed class InputDataManager<TData> : ReactiveDictionary<int, TData>, IInputDataManager<TData> where TData : IInputData
{
    public IReadOnlyDictionary<int, TData> Data { get => _items; }
}}