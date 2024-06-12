using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GroundCheck : MonoBehaviour
{
    [field: SerializeField] public float Length { get; private set; }
    public LayerMask targetLayers;

    public RaycastHit Hit => hit;
    private RaycastHit hit;
    
    public bool Grounded { get; private set; }
    
    void FixedUpdate()
    {
        Transform trans = transform;
        Ray r = new Ray { origin = trans.position, direction = -trans.up };
        Grounded = Physics.Raycast(r, out hit, Length, targetLayers);
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) FixedUpdate();
        
        Color c = Grounded ? Color.white : Color.grey;
        c.a = .5f;
        Gizmos.color = c;

        Vector3 pos = transform.position;
        float l = Application.isPlaying ? Hit.distance : Length;
        Gizmos.DrawLine(pos, pos - transform.up * l);
    }
}
