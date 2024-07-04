using UnityEngine;

public class FootballBehaviour : MonoBehaviour
{
    public static FootballBehaviour instance;

    private Rigidbody rb;
    private float maxYPosition = 4.6f;
    private float bounceForce = 1f;
    private float downwardForce = 2f; // Adjust this value as needed

    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    void FixedUpdate()
    {
        // to ensure ball does not go stationary (needs fixing)
        if (rb.velocity.magnitude == 0 || transform.position.y < -3.16)
        {
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }
        // keeping the ball from crossing the boundary
        if (transform.position.y > maxYPosition)
        {
            rb.AddForce(Vector3.down * downwardForce, ForceMode.Impulse);
            Vector3 newPosition = transform.position;
            newPosition.y = maxYPosition;
            transform.position = newPosition;
        }
    }
}
