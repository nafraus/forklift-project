using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "CustomInputAsset/Vector2", fileName = "Vector2InputAsset")]
public class Vector2InputAsset : CustomInputAsset<Vector2>
{
    [SerializeField] private bool clampX;
    [SerializeField] private bool clampY;

    [SerializeField, ShowIf("clampX")] private float xMinValue;
    [SerializeField, ShowIf("clampX")] private float xMaxValue;
    [SerializeField, ShowIf("clampY")] private float yMinValue;
    [SerializeField, ShowIf("clampY")] private float yMaxValue;
    public new void Write(Vector2 vec)
    {
        if (clampX)
        {
            vec.x = Mathf.Clamp(vec.x ,xMinValue, xMaxValue);
        }
        if (clampY)
        {
            vec.y = Mathf.Clamp(vec.y ,yMinValue, yMaxValue);
        }
        //base. uses CustomInputAsset<float>.Write()
        base.Write(vec);
    }
}
