using System;
using System.Collections.Generic;

namespace SFInput {
public sealed class InputDataManager<TData> : IInputDataManager<TData> where TData : IInputData
{
    private readonly Dictionary<int, TData> _data = new();
    private readonly Dictionary<int, Action> _dataChangeActions = new();

    public IReadOnlyDictionary<int, TData> Data { get => _data; }

    public event Action <int, TData> DataAdded;
    public event Action<TData> DataChanged;
    public event Action<int, TData> DataRemoved;

    public bool AddData(int index, TData data)
    {
        if (_data.TryAdd(index, data))
        {
            SubscribeData(index, data);
            DataAdded?.Invoke(index, data);

            return true;
        }

        return false;
    }

    public bool RemoveData(int index)
    {
        if (_data.Remove(index, out var data))
        {
            UnsubscribeData(index, data);
            DataRemoved?.Invoke(index, data);

            return true;
        }

        return false;
    }

    private void SubscribeData(int index, TData data)
    {
        var action = new Action(() => DataChanged?.Invoke(data));

        if (_dataChangeActions.TryAdd(index, action))
        {
            data.Changed += action;
        }
    }

    private void UnsubscribeData(int index, TData data)
    {
        if (_dataChangeActions.Remove(index, out var action))
        {
            data.Changed -= action;
        }
    }
}}