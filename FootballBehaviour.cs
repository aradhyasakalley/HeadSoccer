using UnityEngine;

public class FootballBehaviour : MonoBehaviour
{
    public static FootballBehaviour instance;

    private Rigidbody rb;
    private float maxYPosition = 4.6f;
    private float bounceForce = 1f;

    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ; //| RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude == 0 || transform.position.y < -3.16)
        {
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }
        if (transform.position.y > maxYPosition)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = maxYPosition;
            transform.position = newPosition;
        }

    }
}
