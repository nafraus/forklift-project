using NaughtyAttributes;
using SqdthUtils.Vectors;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RC : MonoBehaviour
{
    /// <summary>
    /// Used to evaluate whether a float is close to zero
    /// </summary>
    private const float KFloatZeroTolerance = .1f;
    /// <summary>
    /// Evaluates whether a float is close to zero using the constant tolerance value.
    /// </summary>
    /// <param name="f"> The float to evaluate. </param>
    /// <returns> True when f is within the tolerance to zero, false otherwise. </returns>
    private bool IsFloatZero(float f) => Mathf.Abs(f) < KFloatZeroTolerance;
    [ReadOnly] public Vector2 movementInput;
    
    /* ==== PUBLIC MEMBERS ==== */
    [Header("Body")] 
    [Tooltip("The distance between the two wheels")]
    public float wheelBase = 1f;
    [Tooltip("Optional offset to shift the wheelbase forward or backwards along the car's body.")]
    public float wheelBaseOffset = 0f;
    
    [Header("Linear Motion")]
    [Tooltip("The rate at which the car's speed changes in the forward/backwards direction under the influence of the player.")]
    public float linearAcceleration = 12f;
    [Tooltip("The top speed of the car in the horizontal plane. Exceeding this speed will result in the user input being ignored.")]
    public float maxSpeed = 32f;
    
    [Header("Angular Motion")]
    [Tooltip("The rate at which the wheels can change direction.")]
    public float steerAcceleration  =  90f;
    [Tooltip("The maximum angle a wheel can turn in either direction. " +
             "Max steering angle of 45 deg results in a turning radius " +
             "equal to the wheelbase.")]
    public float maxSteerAngle = 45f;

    [Tooltip("The scriptable object used for complex input gathering.")]
    [SerializeField] private Vector2InputAsset input;
    
    /// <summary>
    /// Rotation of steering angle.
    /// </summary>
    private Quaternion SteerRot => Quaternion.AngleAxis(steerAngle, rb.transform.up);
    /// <summary>
    /// Wheel forward direction. 
    /// </summary>
    private Vector3 SteerForward => SteerRot * rb.transform.forward;
    /// <summary>
    /// Car turning radius
    /// </summary>
    private float TurningRadius => 
        Mathf.Abs(wheelBase / Mathf.Tan(Mathf.Deg2Rad * maxSteerAngle));
    
    /* ==== PRIVATE MEMBERS ==== */
    /// <summary>
    /// The rigidbody of the RC car.
    /// </summary>
    private Rigidbody rb;
    /// <summary>
    /// The rigidbody's transform
    /// </summary>
    Transform rbTrans;
    /// <summary>
    /// Steering angle: Angle of the wheels.
    /// </summary>
    private float steerAngle;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rbTrans = rb.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Get inputs
        movementInput = input.Read(); 
        
        // ============ TESTING INPUT DEBUG ============
        // if (movementInput.magnitude > 0)
        //     Debug.Log($"X: {movementInput.x} Y: {movementInput.y}");
        
        
        // Update steering value
        if (!IsFloatZero(movementInput
                .x)) // If input is provided this frame
        {
            steerAngle =
                Mathf.Clamp(
                    steerAngle + movementInput.x * steerAcceleration *
                    Time.deltaTime, -maxSteerAngle, maxSteerAngle);
        }
        else // If input is not provided this frame
        {
            // Do half acceleration in opposite direction
            steerAngle = Mathf.Lerp(steerAngle, 0,
                Time.deltaTime * steerAcceleration / 2);

            // If steering angle is close enough to zero, set it to zero;
            if (Mathf.Abs(steerAngle) < 5)
            {
                steerAngle = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        // THERE IS NO GROUNDED CHECK RIGHT NOW, IM DITCHING TONS OF "REALISTIC"
        // MECHANICS IN FAVOR OF JUST GETTING THIS WORKING IN GAME. ALSO OUR NEW
        // DESIGN IS MUCH LESS REALISTIC THAN THE ORIGINAL FORKLIFT IDEA - Keith

        Vector3 horizontalVel = rb.linearVelocity.NegatedDirection(Vector3.up);
        Vector3 forward = rbTrans.forward;
        Vector3 right = rbTrans.right;
        
        // Do linear acceleration
        if (horizontalVel.magnitude < maxSpeed)
        {
            // Add force to rigidbody in steering forward direction
            rb.AddForce(forward * (movementInput.y * linearAcceleration * Time.fixedDeltaTime),
                ForceMode.VelocityChange);
        }
        
        // Adjust rotation to match steering angle
        float forwardSpeed = Vector3.Dot(rb.linearVelocity, forward);
        if (!IsFloatZero(forwardSpeed)) // If XZ velocity aligns with moving forward/backwards
        {
            // Get pivot modifier, helps determine which side of the car to pivot around
            float pivotMod = steerAngle * (forwardSpeed > 0 ? 1 : forwardSpeed < 0 ? -1 : 0);
            Vector3 pos = rbTrans.position;
            Vector3 backAxle = pos - forward * (.5f * wheelBase + wheelBaseOffset);
            Vector3 pivotPoint = backAxle + rbTrans.right * 
                                 ((pivotMod < 0 ? -1 : pivotMod > 0 ? 1 : 0) * TurningRadius); 

            float deltaRot = movementInput.x * steerAcceleration * 
                             Mathf.Clamp(horizontalVel.magnitude, 0, Time.fixedDeltaTime);
            rb.transform.RotateAround(pivotPoint, rbTrans.up, deltaRot);
            
            Debug.DrawLine(pos - rbTrans.forward * (.5f * wheelBase + wheelBaseOffset), 
                pivotPoint, Color.yellow);
        }
        
        // Limit the car's speed in forward direction
        if (Vector3.Dot(horizontalVel, transform.forward) > maxSpeed)
        {
            rb.linearVelocity = transform.forward * maxSpeed;
        }
        
        // Reduce drifting momentum
        rb.AddForce(-rb.linearVelocity.IsolatedDirection(right) *
                    Mathf.Clamp(forwardSpeed / maxSpeed, 1, 2), 
            ForceMode.VelocityChange);
    }

    #if UNITY_EDITOR
    
    private void OnDrawGizmos()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();

        Transform rbTrans = rb.transform;
        Vector3 pos = rbTrans.position;
        
        // Draw steering forward line
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(pos, pos + SteerForward * 5);
        
        // Draw axle lines
        Vector3 halfBaseDelta = rb.transform.forward * (.5f * wheelBase + wheelBaseOffset);
        Vector3 fPos = pos + halfBaseDelta;
        Vector3 bPos = pos - halfBaseDelta;
        Vector3 right = rbTrans.right.normalized * 2f;
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(fPos + right, fPos - right);
        Gizmos.DrawLine(bPos + right, bPos - right);
    }
    
    #endif
}
