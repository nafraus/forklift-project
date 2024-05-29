using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [Header("Rotating Platform")]
    public float rotateSpeed = 10;

    private Vector3 upDirection;

    private Rigidbody rb;

    private void Start()
    {
        upDirection = Vector3.up;
        rb = GetComponent<Rigidbody>();
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



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 2f);
    }
}
