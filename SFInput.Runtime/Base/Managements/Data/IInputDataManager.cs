using System;
using System.Collections.Generic;

namespace SFInput {
public interface IInputDataManager<TData> where TData : IInputData
{
    IReadOnlyDictionary<int, TData> Data { get; }

    event Action<int, TData> DataAdded;
    event Action<TData> DataChanged;
    event Action<int, TData> DataRemoved;

    void AddData(int index, TData data);
    void RemoveData(int index);
}}