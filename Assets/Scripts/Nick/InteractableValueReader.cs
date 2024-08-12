using System;
using UnityEngine;

public class InteractableValueReader : MonoBehaviour
{
    [SerializeField] private HingeJoint joint;

    [SerializeField] private FloatInputAsset inputAsset;
    
    [SerializeField] private bool useDynamicLimits = false; //Can't remember what this is used for :(

    private float upperLimit;
    private float lowerLimit;

    private void Start()
    {
        upperLimit = joint.limits.max;
        lowerLimit = joint.limits.min;

        inputAsset.ValueChangedAction += LogValue;
        //
    }

    private void Update()
    {
        //Gets a value from 0-1 of where our joint is angled between the min and max
        float val = Mathf.InverseLerp(lowerLimit, upperLimit, joint.angle);
        
        //Converts the 0-1 value to a -1 to 1 value
        val -= 0.5f;
        val *= 2;
        
        //Writes the value
        inputAsset.Write(val);
    }

    void LogValue(float value)
    {
        Debug.Log(value);
    }
}
