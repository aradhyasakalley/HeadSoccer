using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOneController : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction moveAction;
    Rigidbody rb;

    public float moveSpeed = 100f;
    public float jumpForce = 10f;
    private bool isGrounded = true;
    private float fixedZPosition;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["P1Move"];
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        fixedZPosition = transform.position.z; // Record the initial z position
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(direction.x, 0f, 0f) * moveSpeed * Time.deltaTime; // No movement on the z-axis

        // Apply horizontal movement
        rb.MovePosition(rb.position + move);

        // Check for jump input
        if (isGrounded && direction.y > 0)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // Ensure z position remains fixed
        Vector3 currentPosition = rb.position;
        currentPosition.z = fixedZPosition;
        rb.position = currentPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
