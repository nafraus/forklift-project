using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityTrack : MonoBehaviour
{
    public Vector3 Velocity { get; private set; }

    private Vector3 lastPosition;
    private Vector3 currentPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
    }
    
    void FixedUpdate()
    {
        currentPosition = transform.position;
        Velocity = (currentPosition - lastPosition) / Time.fixedTime;
        lastPosition = currentPosition;
    }
}
