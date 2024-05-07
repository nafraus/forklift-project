using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HapticData<T> : GenericHapticData
{
    [SerializeField] protected T intensity;
    [SerializeField] protected XRHapticsApplyType type;
    [SerializeField] protected float duration;
    [SerializeField] protected int operationLayer;
    [SerializeField] protected int priorityLayer;

    public T Value { get => intensity; }

    #region IHapticData Implementation
    public XRHapticsApplyType Type { get => type; }
    public float MaxDuration { get => duration; }
    #endregion

    public override abstract HapticImpulse GenerateImpulse(XRHapticsManagerPro manager);
}