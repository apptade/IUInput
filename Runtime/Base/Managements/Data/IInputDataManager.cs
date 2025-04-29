using System;
using System.Collections.Generic;

namespace AFUInput {
public interface IInputDataManager<TData> where TData : IInputData
{
    IReadOnlyDictionary<int, TData> Data { get; }

    event Action<int, TData> DataAdded;
    event Action<TData> DataChanged;
    event Action<int, TData> DataRemoved;

    bool AddData(int index, TData data);
    bool RemoveData(int index);
}}