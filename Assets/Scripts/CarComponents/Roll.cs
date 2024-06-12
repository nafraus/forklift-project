using UnityEngine;
using UnityEngine.Serialization;

public class Roll : MonoBehaviour
{
    [field: SerializeField]
    [field: Tooltip("X represents % of velocity in roll axis. Y represents % " +
                    "of speed applied as deceleration in roll direction. " +
                    "\nLower deceleration tends to make sense at higher velocities.")]
    public AnimationCurve NaturalDeceleration { get; private set; }
    
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
        dir = trans.forward;
        
        if (!gc.Grounded) return;

        // Calculate speed
        Vector3 vel = rb.GetPointVelocity(pos);
        float speed = Vector3.Dot(vel, dir);
        
        // Calculate force mult
        forceMultiplier = NaturalDeceleration.Evaluate(
            Mathf.Abs(speed / vel.magnitude)
        );
        
        // Create force vector
        Vector3 force = -dir * (speed * forceMultiplier);
        
        // Add force to car rigidbody
        rb.AddForceAtPosition(force, pos, ForceMode.VelocityChange);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pos, pos + dir * forceMultiplier);
    }
}