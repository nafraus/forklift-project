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
    [SerializeField] private bool limitRange = true;
    [SerializeField] private HingeJoint hinge;
    [SerializeField] private TMP_Text text;
    [SerializeField] private float maxSumAngleRange = 360;
    [SerializeField] private UnityEvent<float> onHingeAngleChange;
    [SerializeField] private UnityEvent<float> onHingeSum01Change;
    [SerializeField] private UnityEvent onHingeAngleMin;
    [SerializeField] private UnityEvent onHingeAngleMax;
    private bool _canMaxEvent = true;
    private bool _canMinEvent = true;
    private float _previousAngle = 0;
    private float _hingeSumDelta = 0;

    public float hingeSum01 => _hingeSumDelta / maxSumAngleRange;

    private void FixedUpdate()
    {
        float delta = hinge.angle - _previousAngle;
        if (Mathf.Abs(delta) > 270f)
        {
            if (delta > 180) 
                delta -= 360f;
            else 
                delta += 360;
        }
        _hingeSumDelta += delta;
        _previousAngle = hinge.angle;
        onHingeSum01Change?.Invoke(hingeSum01);
        onHingeAngleChange?.Invoke(delta);
        text.text = hingeSum01.ToString("P0");

        if (!limitRange) return;
        
        if (_hingeSumDelta < maxSumAngleRange && _hingeSumDelta >= 0)
        {
            if (hinge.useLimits) UnfreezeHandle();
        }
        else if (!hinge.useLimits)
        {
            if (_hingeSumDelta < 1 && _canMinEvent)
            {
                onHingeAngleMin?.Invoke();
                _canMinEvent = false;
                _canMaxEvent = true;
            }
            else if (_canMaxEvent)
            {
                onHingeAngleMax?.Invoke();
                _canMinEvent = true;
                _canMaxEvent = false;
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
