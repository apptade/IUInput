using System.Collections.Generic;

namespace SFInput.Screen {
public sealed class MousePinchInputAdder : InputAdder<MousePinchInputController, PinchInputData>
{
    protected override void Awake()
    {
        AddableManager.DataManager.AddData(0, 1);
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        this.ForEachController(c => c.Dispose());
    }

    protected override IReadOnlyDictionary<int, MousePinchInputController> GetControllers()
    {
        return new Dictionary<int, MousePinchInputController>()
        {
            { 0, new(AddableManager.DataManager.Data[0]) }
        };
    }
}}