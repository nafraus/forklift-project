using System;
using UnityEngine;

public abstract class HapticImpulse
{
    //Rename Time to duration
    public Action<HapticImpulse> removeAction { get; set; }
    public abstract float Value { get; }
    public float Time { get; private set; }

    public abstract float MaxTime { get; }

    public XRHapticsApplyType ImpulseType { get; protected set; }

    public void AddTime(float t)
    {
        Time += t;
        Debug.Log(Time);
        if(Time >= MaxTime) removeAction.Invoke(this);
    }
}

