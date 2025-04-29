using System;
using UnityEngine;

namespace IUInput.Screen {
public abstract class PinchInputController : InputController, IDisposable
{
    public readonly PinchInputData PinchData;
    public Vector2 SettableMiddlePosition { get; protected set; }

    public PinchInputController(PinchInputData pinchData)
    {
        PinchData = pinchData;
    }

    public virtual void Dispose() {}
}}