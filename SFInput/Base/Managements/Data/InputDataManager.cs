using System;
using System.Collections.Generic;

namespace SFInput {
public sealed class InputDataManager<TData> : IInputDataManager<TData> where TData : IInputData
{
    private readonly Dictionary<int, TData> _data = new();
    private readonly Dictionary<int, Action> _dataChangeActions = new();

    public IReadOnlyDictionary<int, TData> Data { get => _data; }
    public event Action<TData> AnyDataChanged;

    public void AddData(int index, TData data)
    {
        if (_data.TryAdd(index, data))
        {
            SubscribeData(index, data);
        }
    }

    public void RemoveData(int index)
    {
        if (_data.Remove(index, out var data))
        {
            UnsubscribeData(index, data);
        }
    }

    private void SubscribeData(int index, TData data)
    {
        Action action = () => AnyDataChanged?.Invoke(data);
        _dataChangeActions.Add(index, action);
        data.Changed += action;
    }

    private void UnsubscribeData(int index, TData data)
    {
        _dataChangeActions.Remove(index, out var action);
        data.Changed -= action;
    }
}}