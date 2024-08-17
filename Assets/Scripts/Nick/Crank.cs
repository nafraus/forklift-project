using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using BezierSolution;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Crank : MonoBehaviour
{
    [SerializeField] private HingeJoint hinge;
    [SerializeField] private TMP_Text text;
    [SerializeField] private float maxSumAngleRange = 360;
    [SerializeField] private UnityEvent<float> onHingeAngleChange;
    [SerializeField] private UnityEvent onHingeAngleMin;
    [SerializeField] private UnityEvent onHingeAngleMax;
    private bool canMaxEvent = true;
    private bool canMinEvent = true;
    private float hingeAngleRaw => (hinge.angle + 180) / 360f;
    
    private float previousAngle = 0;
    public float HingeSumDelta { get; private set; } = 0;

    public float RawHingeSum =>
        (HingeSumDelta) / // + maxSumAngleRange * .5f) /
        maxSumAngleRange;

    private void Update()
    {
        float delta = hinge.angle - previousAngle;
        if (Mathf.Abs(delta) > 270f)
        {
            if (delta > 180) 
                delta -= 360f;
            else 
                delta += 360;
        }
        HingeSumDelta += delta;
        previousAngle = hinge.angle;
        text.text = RawHingeSum.ToString("P0");
        
        if (HingeSumDelta < maxSumAngleRange && HingeSumDelta >= 0)
        {
            if (hinge.useLimits) UnfreezeHandle();
            
            onHingeAngleChange?.Invoke(RawHingeSum);
        }
        else if (!hinge.useLimits)
        {
            if (HingeSumDelta < 1 && canMinEvent)
            {
                onHingeAngleMin?.Invoke();
                canMinEvent = false;
                canMaxEvent = true;
            }
            else if (canMaxEvent)
            {
                onHingeAngleMax?.Invoke();
                canMinEvent = true;
                canMaxEvent = false;
            }
            
            FreezeHandle();
        }
    }

    public void UnfreezeHandle() => hinge.useLimits = false;
    public void FreezeHandle()
    {
        hinge.limits = new JointLimits
        {
            min = Mathf.Floor(hinge.angle),
            max = Mathf.Ceil(hinge.angle)
        };
        hinge.useLimits = true;
    }
}
