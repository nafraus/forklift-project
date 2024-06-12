using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform target;
    public Vector3 targetLocalFollowOffset;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (target == null) target = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (rb != null) rb.MovePosition(target.TransformPoint(targetLocalFollowOffset));
        else transform.position = target.TransformPoint(targetLocalFollowOffset);
    }
}
