using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform[] waypoints;


    [Header("Moving Platform Details")]
    public float moveSpeed = 5f;
    public float endWaitTime = 2f;
    public float WPProximity = .5f; //How close does the platform need to be in order to increase index


    private int currentIndex = 0;

    private bool canMove = true;

    private Rigidbody rb;

    private Transform originalParent;

    private Transform playerHolder;

    private void Start()
    {
        transform.position = waypoints[currentIndex].position;
        rb = GetComponent<Rigidbody>();

        GameObject empty = new GameObject("Player Holder");

        playerHolder = empty.transform;
        playerHolder.transform.SetParent(transform);
    }


    void FixedUpdate()
    {
        if (canMove)
        {
            MovePlatform();
        }
    }

    private void MovePlatform()
    {
        if (Vector3.Distance(waypoints[currentIndex].transform.position, transform.position) < WPProximity)
        {
            currentIndex++;
            if (currentIndex >= waypoints.Length)
            {
                StartCoroutine(WaitAtEnd());
                return;
            }
        }

        if (canMove)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, waypoints[currentIndex].transform.position, moveSpeed * Time.deltaTime);
            rb.MovePosition(newPosition);

        }
    }

    private IEnumerator WaitAtEnd()
    {
        canMove = false;

        yield return new WaitForSeconds(endWaitTime);

        //Reverse Waypoint Array and set index to 0
        System.Array.Reverse(waypoints);
        currentIndex = 0;

        canMove = true;

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


    private void OnDrawGizmos()
    {
        //So it doesn't spaz out when making the moving platform
        if (waypoints == null || waypoints.Length < 2)
        {
            return;
        }

        Gizmos.color = Color.green;
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }
    }

}
