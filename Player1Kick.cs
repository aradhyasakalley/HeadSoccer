using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1Kick : MonoBehaviour
{
    public Transform foot;
    public float kickAngle = 60f; // Increase the kick angle for a more forceful kick
    public float kickDuration = 0.05f; // Decrease the kick duration for a faster kick

    private PlayerInput playerInput;
    private InputAction kickAction;
    private Quaternion originalRotation;

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

        // Rotate the foot back to the original angle
        elapsedTime = 0f;
        while (elapsedTime < kickDuration / 2)
        {
            float angle = Mathf.Lerp(kickAngle, 0, elapsedTime / (kickDuration / 2));
            foot.localRotation = Quaternion.Euler(0, 0, angle) * originalRotation;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foot.localRotation = originalRotation;
    }
}
