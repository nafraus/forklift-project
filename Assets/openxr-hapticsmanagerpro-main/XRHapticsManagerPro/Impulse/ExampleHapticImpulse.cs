/// <summary>
/// Example File of how to setup HapticImpulse for new types
/// </summary>

public class ExampleHapticImpulse : HapticImpulse
{
    public ExampleHapticImpulse(HapticData<ExampleType> scriptableObject, XRHapticsManagerPro manager) //<< CHANGE ExampleHapticImpulse TO YOURTYPEHapticImpulse AND ExampleType TO NEW TYPE
    {
        data = scriptableObject;
        removeAction += manager.RemoveHaptic;
        ImpulseType = scriptableObject.Type;
        manager.AddTimeStepAction += AddTime;
    }
    public HapticData<ExampleType> data { get; set; } //<< REPLACE EXAMPLE TYPE WITH NEW TYPE

    public override float MaxTime { get => data.MaxDuration; }

    public override float Value
    {
        get
        {
            return data.Value.sample; //<< CHANGE THIS TO RETURN AN EVALUATED FLOAT FROM NEW TYPE. SEE CurveHapticImpulse FOR EXAMPLE
        }
    }
}