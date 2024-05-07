using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/HapticData/Flat Value", fileName = "FlatValueHapticData", order = 0)]
public class FloatHapticDataSO : HapticData<float>
{
    public override HapticImpulse GenerateImpulse(XRHapticsManagerPro manager)
    {
        return new FloatHapticImpulse(this, manager);
    }
}
