using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomInputAsset/float", fileName = "FloatInputAsset")]
public class FloatInputAsset : CustomInputAsset<float>
{
    [SerializeField] public float MinValue;

    public float MaxValue;

    public new void Write(float number)
    {
        //base. uses CustomInputAsset<float>.Write()
        base.Write(Mathf.Clamp(number, MinValue, MaxValue));
    }
}
