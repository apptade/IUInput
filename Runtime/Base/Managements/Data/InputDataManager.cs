using System;
using System.Collections.Generic;

namespace IUInput {
public sealed class InputDataManager<TData> : IInputDataManager<TData> where TData : IInputData
{
    private readonly Dictionary<int, TData> _data = new();

    public event Action <int, TData> DataAdded;
    public event Action<int, TData> DataRemoved;

    public IReadOnlyDictionary<int, TData> Data { get => _data; }

    public bool AddData(int key, TData data)
    {
        if (data is null) return false;

        if (_data.TryAdd(key, data))
        {
            DataAdded?.Invoke(key, data);
            return true;
        }

        return false;
    }

    public bool RemoveData(int key)
    {
        if (_data.Remove(key, out var data))
        {
            DataRemoved?.Invoke(key, data);
            return true;
        }

        return false;
    }
}}