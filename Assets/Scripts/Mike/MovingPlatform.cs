using System;
using System.Collections;
using BezierSolution;
using UnityEngine;

[RequireComponent(typeof(BezierWalkerWithSpeed))]
public class MovingPlatform : MonoBehaviour
{
    [Header("Moving Platform Details")] 
    public bool autoMove = true;
    public float moveSpeed = 5f;
    public float endWaitTime = 2f;
    
    private BezierWalkerWithSpeed walker;
    
    private Transform originalParent;
    private Transform playerHolder;

    private void Start()
    {
        walker = GetComponent<BezierWalkerWithSpeed>();
        walker.NormalizedT = 0;
        walker.travelMode = TravelMode.Once;
        walker.onPathCompleted.AddListener(DoPauseAndReverse);
        walker.speed = autoMove ? moveSpeed : 0;

        GameObject empty = new GameObject("Player Holder");
        playerHolder = empty.transform;
        playerHolder.transform.SetParent(transform);
    }

    public void SetPathProgress(float normalizedT)
    {
        walker.NormalizedT = normalizedT;
    }
    
    private void DoPauseAndReverse()
    {
        if (autoMove) StartCoroutine(PauseAndReverse());
    }

    private IEnumerator PauseAndReverse()
    {
        // Stop moving platform
        walker.speed = 0;
        
        // Wait for time
        yield return new WaitForSeconds(endWaitTime);
        
        // Start moving again
        walker.speed = moveSpeed * walker.NormalizedT switch
        {
            >= .99f => -1,
            <= .01f =>  1,
            _       =>  0
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (originalParent == null)
            {
                originalParent = other.gameObject.transform.parent;
            }

            other.gameObject.transform.SetParent(playerHolder);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(originalParent);
            
            originalParent = null;
            
        }
    }
}
