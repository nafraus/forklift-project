using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Crank : MonoBehaviour
{
    [SerializeField] private HingeJoint hinge;
    [SerializeField] private TMP_Text text;


    private void Update()
    {
        text.text = ((int)hinge.angle).ToString();
    }
}
