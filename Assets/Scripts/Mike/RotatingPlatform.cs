using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [Header("Rotating Platform")] 
    public bool autoMove = true;
    public float rotateSpeed = 10;
    public float crankMultiplier = .1f;

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
        if (autoMove)
        {
            Quaternion deltaRotation = Quaternion.AngleAxis(rotateSpeed * Time.deltaTime, upDirection);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }

    public void RotatePlatform(float delta)
    {
        Debug.Log(delta + ": " + delta * crankMultiplier);
        
        Quaternion deltaRotation = 
            Quaternion.AngleAxis(delta * crankMultiplier, upDirection);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    public void HoldGameObject(Transform go)
    {
        if (!go.CompareTag("Player")) return;
        
        if (originalParent == null)
            originalParent = go.gameObject.transform.parent;
        
        go.SetParent(playerHolder);
    }

    public void LetGo(Transform go)
    {
        if (!go.CompareTag("Player")) return;
        
        go.SetParent(originalParent);

        originalParent = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 2f);
    }
}
