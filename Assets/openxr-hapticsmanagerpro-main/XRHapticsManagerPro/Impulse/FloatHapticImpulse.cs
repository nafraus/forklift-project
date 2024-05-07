public class FloatHapticImpulse : HapticImpulse
{
    public FloatHapticImpulse(HapticData<float> scriptableObject, XRHapticsManagerPro manager)
    {
        data = scriptableObject;
        removeAction += manager.RemoveHaptic;
        ImpulseType = scriptableObject.Type;
        manager.AddTimeStepAction += AddTime;
    }
    public HapticData<float> data { get; set; }

    public override float MaxTime { get => data.MaxDuration; }

    public override float Value
    {
        get
        {
            return data.Value;
        }
    }
}