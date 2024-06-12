using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Shock : MonoBehaviour
{
    [Tooltip("Force of shock while under compression. Used to extend back to rest length.")] [Min(1)]
    public float springStrength = 1f;
    [Tooltip("Force of damping. Used to extend back to length.")] [Min(1)]
    public float dampingStrength = 1f;
    
    private GroundCheck gc;
    private Rigidbody rb;
    private Vector3 pos;
    private Vector3 dir;
    
    /// <summary>
    /// Resting length of the spring.
    /// </summary>
    public float RestitutionLength => gc.Length;
    private float forceMult;
    private float offset;

    private void Awake()
    {
        gc = GetComponent<GroundCheck>();
        rb = GetComponentInParent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        Transform trans = transform;
        pos = trans.position;
        dir = trans.up;
        
        if (!gc.Grounded) return;
        
        // Calculate offset
        offset = RestitutionLength - gc.Hit.distance;

        // Calculate force multiplier
        Vector3 vel = rb.GetPointVelocity(pos);
        float springVel = Vector3.Dot(vel, dir);
        float springForce = offset * springStrength;
        float dampingForce = springVel * dampingStrength; 
        forceMult = springForce - dampingForce;
        
        // Create force vector
        Vector3 force = trans.up * Mathf.Clamp(forceMult, 0, rb.mass);
        
        // Add force to car rigidbody
        rb.AddForceAtPosition(force, pos, ForceMode.Impulse);
    }
    
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        
        Gizmos.color = Color.green;
        Vector3 force = dir * Mathf.Clamp(forceMult, 0, rb.mass);
        Gizmos.DrawLine(pos, pos + force);
    }
}