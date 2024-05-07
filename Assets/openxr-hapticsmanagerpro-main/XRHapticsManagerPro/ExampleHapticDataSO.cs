/// <summary>
/// Example File of how to setup HapticData for new types
/// </summary>
using UnityEngine;

//[CreateAssetMenu(menuName = "ScriptableObject/HapticData/Example", fileName = "ExampleHapticData", order = 1)]
//UNCOMMENT AND CHANGE THE MENUNAME AND FILENAME
public class ExampleHapticDataSO : HapticData<ExampleType> //REPLACE EXAMPLE TYPE WITH NEW TYPE
{
    public override HapticImpulse GenerateImpulse(XRHapticsManagerPro manager)
    {
        return new ExampleHapticImpulse(this, manager); //REPLACE EXAMPLEHAPTICIMPULSE WITH THE HAPTIC IMPULSE YOU HAVE CREATED
    }
}

public struct ExampleType { public float sample; } // DELETE THIS LINE
