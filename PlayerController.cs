using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction moveAction;
    Rigidbody rb;
    
    public static PlayerController instance;
    public float moveSpeed = 100f;
    public float jumpForce = 10f;
    private bool isGrounded = true;
    private float fixedZPosition;

    public int player;

    void Start()
    {
        instance = this;
        playerInput = GetComponent<PlayerInput>();
        if (player == 1)
        {
            moveAction = playerInput.actions["P1Move"];
        }
        else
        {
            moveAction = playerInput.actions["P2Move"];
        }

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        fixedZPosition = transform.position.z;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(direction.x, 0f, 0f) * moveSpeed * Time.deltaTime;

        Vector3 newPosition = rb.position + move;

        // Clamp the x position within the specified range
        newPosition.x = Mathf.Clamp(newPosition.x, -8.71f, 8.60f);

        // Maintain the fixed z position
        newPosition.z = fixedZPosition;

        rb.MovePosition(newPosition);

        if (isGrounded && direction.y > 0)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
