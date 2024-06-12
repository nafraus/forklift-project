using UnityEngine;

[CreateAssetMenu(fileName = "NewData", menuName = "CustomScriptableObject")]
public class CarStats : ScriptableObject
{
     [field: Header("Steering")]
     [field: SerializeField] [field: Range(8,16)] 
     public float TurningRadius { get; private set; } = 36;

     [field: Header("Power")]
     [field: SerializeField] [field: Range(8, 16)]
     public float RpmLimit { get; private set; } = 7500; 
     [field: SerializeField] [field: Range(8,16)] 
     public float[] GearRatios { get; private set; } = // Starts with reverse, skips neutral, 1st-8th 
          { 3.99f, 4.89f, 3.17f, 2.15f, 1.56f, 1.18f, 0.94f, 0.76f, 0.61f };
}
