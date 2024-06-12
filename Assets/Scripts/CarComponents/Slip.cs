using System;
using UnityEngine;

public class Slip : MonoBehaviour
{
    [field:SerializeField] 
    [field: Tooltip("X represents % of velocity in slip axis. Y represents applied traction in slip direction." +
                    "\nLower traction tends to make sense at higher velocities.")]
    public AnimationCurve TractionByVelocity { get; private set; }
    
    [field: SerializeField]
    [field: Tooltip("Flip this wheel's slip direction to its negative X " +
                    "transform direction.")]
    public bool Flipped { get; private set; }
    
    private GroundCheck gc;
    private Rigidbody rb;

    private Vector3 pos;
    private Vector3 dir;
    private float forceMultiplier;
    
    void Awake()
    {
        gc = GetComponent<GroundCheck>();
        rb = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Transform trans = transform;
        pos = trans.position;
        dir = trans.right;
        if (Flipped) dir *= -1;
        
        if (!gc.Grounded) return;
        
        // Calculate speed
        Vector3 vel = rb.GetPointVelocity(pos);
        float speed = Vector3.Dot(vel, dir);
        
        // Calculate force mult
        forceMultiplier = TractionByVelocity.Evaluate(
            Mathf.Abs(speed / vel.magnitude)
        );
        
        // Create force vector
        Vector3 force = -dir * (speed * forceMultiplier);
        
        // Early exit for low force
        if (force.magnitude < .001f) return;
        
        // Add force to car rigidbody
        rb.AddForceAtPosition(force, pos, ForceMode.VelocityChange);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pos, pos + dir * forceMultiplier);
    }
}