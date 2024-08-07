using System;
using SqdthUtils.Vectors;
using UnityEngine;

public class SpoonLift : MonoBehaviour
{
    [Header("Inputs")] 
    public KeyCode gasPetal;
    public KeyCode breakPetal;
    
    [Header("Steering")]
    public float turnSpeed = 3f;
    public float maxSteeringAngle = 17;

    [Header("Vehicle")] 
    public float acceleration = 12f;
    public float deceleration = 18f;
    public float maxSpeed = 12f;
    [field: SerializeField]
    public Wheel[] Wheels { get; private set; } = 
        new Wheel[4];

    private Rigidbody rb;
    private float[] steerAngles = new float[4];
    private Vector2 inputs; // to be replaced once the hybrid input SO exists
    // [field: SerializedField] private HYBRID_INPUT_SO inputSO;
    public Vector2 Inputs => inputs; // To be replaced with a link to the above SO's vector2 movement data 
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputs = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        // ==== TEMPORARY SET UP FOR PC INPUTS ====
        inputs.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(gasPetal))
            inputs.y = 1;
        else if (Input.GetKey(breakPetal))
            inputs.y = -1;
        else
            inputs.y = 0;
    }

    private void FixedUpdate()
    {
        for (var i = 0; i < Wheels.Length; i++)
        {
            // Get wheel data
            Transform wheelTrans = Wheels[i].transform;
            bool canSteer;
            canSteer = Wheels[i].canSteer;
            bool canGas;
            canGas = Wheels[i].canAccelerate;
            bool canBreak = Wheels[i].canDecelerate;
            
            // Do steer
            if (canSteer)
            {
                float steer = Inputs.x;
                if (steer > 0)
                {
                    steerAngles[i] = Mathf.Lerp(steerAngles[i], maxSteeringAngle, turnSpeed * Time.fixedDeltaTime);
                }
                else if (steer < 0)
                {
                    steerAngles[i] = Mathf.Lerp(steerAngles[i], -maxSteeringAngle, turnSpeed * Time.fixedDeltaTime);
                }
                else
                {
                    steerAngles[i] = Mathf.Lerp(steerAngles[i], 0, turnSpeed * Time.fixedDeltaTime);
                }
                steerAngles[i] = Mathf.Clamp(steerAngles[i], -maxSteeringAngle,
                    maxSteeringAngle);
                wheelTrans.rotation = Quaternion.AngleAxis(steerAngles[i], wheelTrans.up);
            }
                
            // Do gas
            if (canGas && Inputs.y > 0 && 
                rb.velocity.NegatedDirection(Vector3.up).magnitude <= maxSpeed)
            {
                rb.AddForceAtPosition((wheelTrans.forward) * acceleration, 
                    wheelTrans.position, ForceMode.Force);
            }
            
            // Do break
            if (canBreak && Inputs.y < 0)
            {
                rb.AddForceAtPosition(
                    -rb.GetPointVelocity(wheelTrans.position).NegatedDirection(Vector3.up) * 
                    deceleration, wheelTrans.position
                );
            }
        }
    }

    [Serializable]
    public class Wheel
    {
        public Transform transform;
        public bool canSteer = false;
        public bool canAccelerate = true;
        public bool canDecelerate = true;
    }
}
