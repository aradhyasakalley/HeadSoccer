using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOneKick : MonoBehaviour
{
    public Transform foot;
    public float kickDuration = 2.0f;
    public float kickForce = 10.0f;
    public float kickRange = 10.0f;
    public float kickAngleZ = 75f;

    private PlayerInput playerInput;
    private InputAction kickAction;
    private Quaternion originalRotation;
    //private bool isKicking = false;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        kickAction = playerInput.actions["P1Kick"];

        if (kickAction == null)
        {
            Debug.LogError("KickP1 action not found in PlayerInput actions.");
            return;
        }

        if (foot == null)
        {
            Debug.LogError("Foot Transform is not assigned in the Inspector.");
            return;
        }

        originalRotation = foot.localRotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Football"))
        {
            Debug.Log("Collision with Football detected.");
            foreach (ContactPoint contact in collision.contacts)
            {
                if (contact.thisCollider.transform == foot)
                {
                    Debug.Log("Kick!!");
                }
                else
                {
                    Debug.Log("Collision, but not with the foot.");
                }
            }
        }
    }
}
