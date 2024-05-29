using System.Collections;
using System.Linq;
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

    //Used for when the player is on the moving platform or not
    private Transform originalPlayerParent;

    private void Start()
    {
        transform.position = waypoints[currentIndex].position;
    }


    void Update()
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
            }
        }

        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentIndex].transform.position, moveSpeed * Time.deltaTime);
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
        //If it's the player, parent it to the moving platform
        if (other.CompareTag("Player"))
        {
            if (originalPlayerParent == null)
            {
                originalPlayerParent = other.transform.parent;
            }
            other.gameObject.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Return the player to its original parent
            other.gameObject.transform.parent = originalPlayerParent;
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
