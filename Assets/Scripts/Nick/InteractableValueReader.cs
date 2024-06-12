using System;
using UnityEngine;

public class InteractableValueReader : MonoBehaviour
{
    [SerializeField] private HingeJoint joint;

    [SerializeField] private FloatInputAsset inputAsset;
    //To be serialized
    private bool useDynamicLimits = false;

    private float upperLimit;
    private float lowerLimit;

    private void Start()
    {
        upperLimit = joint.limits.max;
        lowerLimit = joint.limits.min;

        inputAsset.ValueChangedAction += LogValue;
    }

    private void Update()
    {
        inputAsset.Write(Mathf.InverseLerp(lowerLimit, upperLimit, joint.angle));
    }

    void LogValue(float value)
    {
        Debug.Log(value);
    }
}
