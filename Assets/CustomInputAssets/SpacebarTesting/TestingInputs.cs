using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingInputs : MonoBehaviour
{
    public IntInputAsset asset;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //asset.Write(1);
        }
        else
        {
            //asset.Write(0);
        }
    }
}
