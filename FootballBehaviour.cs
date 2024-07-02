using UnityEngine;

public class FootballBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    private float maxYPosition = 4.6f;
    private float bounceForce = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ; //| RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude == 0 || transform.position.y < -4.0)
        {
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }
        // Constrain y position
        if (transform.position.y > maxYPosition)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = maxYPosition;
            transform.position = newPosition;
        }

    }
}
