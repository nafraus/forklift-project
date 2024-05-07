using UnityEngine;

public class CurveHapticImpulse : HapticImpulse
{
    public CurveHapticImpulse(CurveHapticDataSO scriptableObject, XRHapticsManagerPro manager)
    {
        data = scriptableObject;
        removeAction += manager.RemoveHaptic;
        ImpulseType = scriptableObject.Type;
        manager.AddTimeStepAction += AddTime;
    }
    public HapticData<AnimationCurve> data { get; set; }

    public override float MaxTime { get => data.MaxDuration; }

    public override float Value
    {
        get
        {
            return data.Value.Evaluate(Time);
        }
    }
}