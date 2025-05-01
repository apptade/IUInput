using System;
using System.Collections.Generic;

namespace IUInput {
public sealed class InputDataManager<TData> : IInputDataManager<TData> where TData : IInputData
{
    private readonly Dictionary<int, TData> _data = new();
    private readonly Dictionary<int, Action> _dataChangeActions = new();

    public IReadOnlyDictionary<int, TData> Data { get => _data; }

    public event Action <int, TData> DataAdded;
    public event Action<TData> DataChanged;
    public event Action<int, TData> DataRemoved;

    public bool AddData(int key, TData data)
    {
        if (data is null) return false;

        if (_data.TryAdd(key, data))
        {
            SubscribeData(key, data);
            DataAdded?.Invoke(key, data);

            return true;
        }

        return false;
    }

    public bool RemoveData(int key)
    {
        if (_data.Remove(key, out var data))
        {
            UnsubscribeData(key, data);
            DataRemoved?.Invoke(key, data);

            return true;
        }

        return false;
    }

    private void SubscribeData(int key, TData data)
    {
        var action = new Action(() => DataChanged?.Invoke(data));

        _dataChangeActions.Add(key, action);
        data.Changed += action;
    }

    private void UnsubscribeData(int key, TData data)
    {
        if (_dataChangeActions.Remove(key, out var action))
        {
            data.Changed -= action;
        }
    }
}}