using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [Header("Rotating Platform")]
    public float rotateSpeed = 10;

    private Vector3 upDirection;

    private Rigidbody rb;

    private Transform originalParent;

    private Transform playerHolder;

    private void Start()
    {
        upDirection = Vector3.up;
        rb = GetComponent<Rigidbody>();

        GameObject empty = new GameObject("Player Holder");
        playerHolder = empty.transform;
        playerHolder.transform.SetParent(transform);
    }

    void FixedUpdate()
    {
        RotatePlatform();
    }

    private void RotatePlatform()
    {
        Quaternion deltaRotation = Quaternion.AngleAxis(rotateSpeed * Time.deltaTime, upDirection);

        rb.MoveRotation(rb.rotation * deltaRotation);
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
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 2f);
    }
}
