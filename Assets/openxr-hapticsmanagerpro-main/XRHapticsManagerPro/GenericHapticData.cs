using UnityEngine;

public abstract class GenericHapticData : ScriptableObject
{
    public XRHapticsApplyType Type { get; }
    
    public float MaxDuration { get; }

    public abstract HapticImpulse GenerateImpulse(XRHapticsManagerPro manager);
}

public enum XRHapticsApplyType
{
    Override = 1 <<0,
    Additive = 1<< 1,
    Multiplier = 1 << 2,
}