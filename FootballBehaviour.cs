using UnityEngine;

public class FootballBehaviour : MonoBehaviour
{
    public static FootballBehaviour instance;

    private Rigidbody rb;
    private float maxYPosition = 4.6f;
    private float bounceForce = 1f;
    private float downwardForce = 0.5f;
    private float maxXPosition = 10.95f;
    private float minXPosition = -11.95f;
    private float sideBounceForce = 1f; 

    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude == 0 || transform.position.y < -3.16)
        {
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }

        if (transform.position.y > maxYPosition)
        {
            rb.AddForce(Vector3.down * downwardForce, ForceMode.Impulse);
            Vector3 newPosition = transform.position;
            newPosition.y = maxYPosition;
            transform.position = newPosition;
        }

        if (transform.position.x < minXPosition)
        {
            rb.AddForce(Vector3.right * sideBounceForce, ForceMode.Impulse);
            Vector3 newPosition = transform.position;
            newPosition.x = minXPosition;
            transform.position = newPosition;
        }

        if (transform.position.x > maxXPosition)
        {
            rb.AddForce(Vector3.left * sideBounceForce, ForceMode.Impulse);
            Vector3 newPosition = transform.position;
            newPosition.x = maxXPosition;
            transform.position = newPosition;
        }
    }
}
