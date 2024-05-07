using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/HapticData/Curve", fileName = "CurveHapticData", order = 1)]
public class CurveHapticDataSO : HapticData<AnimationCurve>
{
    public override HapticImpulse GenerateImpulse(XRHapticsManagerPro manager)
    {
        return new CurveHapticImpulse(this, manager);
    }
}
