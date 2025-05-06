using System;
using System.Collections.Generic;

namespace IUInput {
public interface IInputDataManager<TData> where TData : IInputData
{
    IReadOnlyDictionary<int, TData> Data { get; }

    event Action<int, TData> DataAdded;
    event Action<int, TData> DataRemoved;

    bool AddData(int key, TData data);
    bool RemoveData(int key);
}}