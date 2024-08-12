using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public enum DriveSetup { Rear, Front, All }
    [Header("General")] 
    public DriveSetup driveSetup = DriveSetup.All;
    
    public enum FiveSpeedTransmission { Park, Reverse, Neutral, One, Two, Three, Four, Five, Six, Seven }
    [Header("Engine")]
    public FiveSpeedTransmission transmission;
    public float[] gearbox = { 3.91f, 2.29f, 1.58f, 1.18f, 0.94f, 0.79f, 0.62f };
    public float[] kRedlines = { 3.5f, 4.5f, 5.5f, 6.5f, 7.5f, 8.5f, 9.5f };
    [field: ReadOnly] public float EngineKRPM { get; private set; } = 1;
    public float airFlow = 100;

    [Header("Wheels")]
    public Transform[] wheels;
    public Transform frontLeft => wheels[0];
    public Transform frontRight => wheels[1];
    public float breakPadSurfaceArea;

    public float turnSpeed = 3f;
    public float maxSteeringAngle = 17;
    public float tiltAngle = 5;
    
    // General / Tracking
    private Rigidbody rb;
    private float steer;
    private float throttle;
    private float breaking;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Transmission
        if (Input.GetKeyDown(KeyCode.P))
        {
            transmission = FiveSpeedTransmission.Park;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            transmission = FiveSpeedTransmission.Reverse;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0) ||
                 Input.GetKeyDown(KeyCode.N))
        {
            transmission = FiveSpeedTransmission.Neutral;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transmission = FiveSpeedTransmission.One;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transmission = FiveSpeedTransmission.Two;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            transmission = FiveSpeedTransmission.Three;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            transmission = FiveSpeedTransmission.Four;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            transmission = FiveSpeedTransmission.Five;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            transmission = FiveSpeedTransmission.Six;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            transmission = FiveSpeedTransmission.Seven;
        }
        
        // Steer
        steer = Input.GetAxis("Horizontal");
        
        // Throttle
        throttle = Mathf.Clamp01(Input.GetAxis("Vertical"));
        
        // Breaking
        breaking = Mathf.Clamp01(-Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        // Steering
        if (frontLeft != null)
        {
            frontLeft.localRotation = Quaternion.Slerp(frontLeft.localRotation,
                Quaternion.Euler(0, steer * maxSteeringAngle, -tiltAngle), 
                turnSpeed * Time.fixedDeltaTime);
        }
        if (frontRight != null)
        {
            frontRight.localRotation = Quaternion.Slerp(frontRight.localRotation,
                Quaternion.Euler(0, steer * maxSteeringAngle, tiltAngle), 
                turnSpeed * Time.fixedDeltaTime);
        }
        
        // Engine combustion
        if (throttle > 0)
        {
            EngineKRPM =
                Mathf.Clamp(
                    EngineKRPM + throttle * airFlow * Time.fixedDeltaTime, 
                    1, kRedlines[(int)transmission]);
        }
        else
        {
            if (EngineKRPM <= 1f)
            {
                EngineKRPM = 1;
            }
            else
            {
                EngineKRPM -= EngineKRPM * Time.fixedDeltaTime;
            }
        }
        
        // Acceleration through transmission and wheels
        switch (transmission)
        {
            case FiveSpeedTransmission.Park:
            case FiveSpeedTransmission.Neutral:
            {
                rb.AddForce(-new Vector3(rb.velocity.x, 0, rb.velocity.z), ForceMode.Acceleration);
                break;
            }
            
            case FiveSpeedTransmission.Reverse:
            {
                if (throttle > 0)
                {
                    float accel = EngineKRPM * gearbox[0];

                    ApplyForceToWheels(-accel);
                }

                break;
            }
            
            default:
            {
                if (throttle > 0)
                {
                    float accel = EngineKRPM * gearbox[(int)transmission - 3];

                    ApplyForceToWheels(accel);
                }

                break;
            }
        }
        
        // Breaking through break pads and wheels 
        if (breaking > 0)
        {
            float breakingForce = Mathf.Clamp(breaking * breakPadSurfaceArea, 
                0, rb.velocity.magnitude);

            for (int i = 0; i < wheels.Length; i++)
            {
                if (wheels[i].GetComponent<GroundCheck>().Grounded)
                {
                    Vector3 pos = wheels[i].position;
                    rb.AddForceAtPosition(
                        -wheels[i].forward * breakingForce,
                        pos, ForceMode.Acceleration);
                }
            }
        }
    }

    public void ApplyForceToWheels(float force)
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            bool valid = driveSetup switch
            {
                DriveSetup.Front => i < 2,
                DriveSetup.Rear => i >= 2,
                _ => true
            };
            
            if (valid && wheels[i].GetComponent<GroundCheck>().Grounded)
            {
                Vector3 pos = wheels[i].position;
                rb.AddForceAtPosition(
                    wheels[i].forward * force,
                    pos, ForceMode.Acceleration);
            }
        }
    }
}
