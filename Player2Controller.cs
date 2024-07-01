using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Controller : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction moveAction;
    Rigidbody rb;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isGrounded = true;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Player2Move");
        rb = GetComponent<Rigidbody>(); 
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        Debug.Log("Player 2 Moving");
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(direction.x, 0, 0) * moveSpeed * Time.deltaTime;
        transform.position += move;
        if (isGrounded && direction.y > 0) 
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        /*else if (isGrounded && direction.y < 0) // Jump when pressing down
        {
            rb.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }*/
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
