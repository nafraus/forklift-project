using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public IntInputAsset asset;

    private void Start()
    {
        asset.ValueChangedAction += ReadAsset;
    }

    private void ReadAsset(int value)
    {
        Debug.Log(value);
    }
}
