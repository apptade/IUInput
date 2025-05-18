using System.Collections.Generic;

namespace IUInput {
public interface IInputDataManager<TData> : IDictionaryHandler<int, TData> where TData : IInputData
{
    IReadOnlyDictionary<int, TData> Data { get; }
}}