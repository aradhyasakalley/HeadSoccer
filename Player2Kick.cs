using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Kick : MonoBehaviour
{
    public Transform foot;
    public float kickAngle = 60f;
    public float kickDuration = 0.005f;
    public float kickForce = 1000.0f;
    public float kickRange = 1.0f;

    private PlayerInput playerInput;
    private InputAction kickAction;
    private Quaternion originalRotation;
    private bool isKicking = false;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        kickAction = playerInput.actions.FindAction("KickP1");

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
        kickAction.performed += ctx => RotateFoot();
    }

    void RotateFoot()
    {
        if (foot != null)
        {
            Debug.Log("Kicking the ball !!!");
            StartCoroutine(KickRoutine());
        }
        else
        {
            Debug.Log("Foot is Null");
        }
    }

    IEnumerator KickRoutine()
    {
        isKicking = true;
        ApplyKickForce();

        float elapsedTime = 0f;
        while (elapsedTime < kickDuration / 2)
        {
            float angle = Mathf.Lerp(0, kickAngle, elapsedTime / (kickDuration / 2));
            foot.localRotation = Quaternion.Euler(0, 0, angle) * originalRotation;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foot.localRotation = Quaternion.Euler(0, 0, kickAngle) * originalRotation;
        yield return new WaitForSeconds(kickDuration / 2);

        elapsedTime = 0f;
        while (elapsedTime < kickDuration / 2)
        {
            float angle = Mathf.Lerp(kickAngle, 0, elapsedTime / (kickDuration / 2));
            foot.localRotation = Quaternion.Euler(0, 0, angle) * originalRotation;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foot.localRotation = originalRotation;
        isKicking = false;
    }

    void ApplyKickForce()
    {
        Collider[] hitColliders = Physics.OverlapSphere(foot.position, kickRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Football"))
            {
                Rigidbody ballRigidBody = hitCollider.GetComponent<Rigidbody>();
                if (ballRigidBody != null)
                {
                    Vector3 forceDirection = foot.forward;
                    ballRigidBody.AddForce(forceDirection * kickForce, ForceMode.Impulse);
                    Debug.Log("Ball kicked with force: " + kickForce);
                }
                else
                {
                    Debug.Log("Football is not a rigidbody!");
                }
            }
        }
    }
}
